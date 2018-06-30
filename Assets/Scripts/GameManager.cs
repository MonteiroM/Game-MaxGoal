using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //Bola
   [SerializeField] private GameObject bola;
    private int bolasNum = 2;
    private bool bolaMorreu = false;
    private int bolasEmCena = 0;
    private Transform pos;//posição

    public int tiro = 0;

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
    }


    void Start () {

        ScoreManager.instance.GameStartScoreM();
	}
	
	// Update is called once per frame
	void Update () {
        ScoreManager.instance.UpdateScore();
        UiManager.instance.UpdateUI();

        NascBolas();   

    }

    //Método para o respaw de bolas 
    void NascBolas() {
        if (bolasNum > 0 && bolasEmCena == 0) {
            Instantiate(bola, new Vector2(pos.position.x,pos.position.y), Quaternion.identity);            
            bolasEmCena += 1;
            tiro = 0;
        }

    }

    void Carrega(Scene cena, LoadSceneMode modo) {
        pos = GameObject.Find("posStart").GetComponent<Transform>();

    }
    
}
