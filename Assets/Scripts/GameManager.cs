using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //Bola
   [SerializeField] private GameObject bola;
    public int bolasNum = 2;
    public bool bolaMorreu = false;
    public int bolasEmCena = 0;
    public Transform pos;//posição
    public bool win;
    public int tiro = 0;
    public bool jogoComecou;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
        pos = GameObject.Find("posStart").GetComponent<Transform>();
    }


    void Start () {

        ScoreManager.instance.GameStartScoreM();
        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
        ScoreManager.instance.UpdateScore();
        UiManager.instance.UpdateUI();

        NascBolas();

        if (bolasNum <= 0) {
            GameOver();
        }

        if (win) {
            WinGame();
        }

    }

    //Método para o respaw de bolas 
    void NascBolas() {

        if (OndeEstou.instance.fases >= 3)
        {

            if (bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 0.05f)
            {

                Instantiate(bola, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else {    
            
                if (bolasNum > 0 && bolasEmCena == 0)
                {
                    Instantiate(bola, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                    bolasEmCena += 1;
                    tiro = 0;
                }
            
        }

    }

    void Carrega(Scene cena, LoadSceneMode modo) {
        if (OndeEstou.instance.fases != 4) {
            pos = GameObject.Find("posStart").GetComponent<Transform>();
            StartGame();
        }
    }

    void GameOver() {

        UiManager.instance.GameOverUI();
        jogoComecou = false;
    }

    void WinGame() {
        UiManager.instance.WinGameUI();
        jogoComecou = false;
    }

    void StartGame() {
        jogoComecou = true;
        bolasNum = 2;
        bolasEmCena = 0;
        win = false;
        UiManager.instance.StartUi();
    }
    
}
