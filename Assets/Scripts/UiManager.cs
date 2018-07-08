using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public static UiManager instance;
    private Text pontosUI,bolasUI;
    [SerializeField]
    private GameObject losePainel,winPainel,pausePainel;
    [SerializeField]
    private Button pauseBtn,pauseBtn_Return;//botoes de pause
    [SerializeField]
    private Button jogarNovBtn;//botão de jogar novamento abaixo do botao de pause
    [SerializeField]
    private Button novamenteBtnLose, levelBtnLose;//Botões Lose
    private Button novamenteBtnWin, levelBtnWin, avancarBtnWin;//Botões Lose

    public int moedasNumAntes, moedasNumDepois, resultado;

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
        PegaDados();

    }

    void Carrega(Scene cena, LoadSceneMode modo) {
        PegaDados();
        
    }

    void PegaDados() {

        if (OndeEstou.instance.fases != 1)
        {
            //Elementos da UI PontosUI== Moedas, BolasUI== Quantidade de bolas
            pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
            bolasUI = GameObject.Find("BolasUI").GetComponent<Text>();
            //Paineis de Lose, Win, Pause
            losePainel = GameObject.Find("Pnl_Lose");
            winPainel = GameObject.Find("Pnl_Win");
            pausePainel = GameObject.Find("Pnl_Pause");
            //Pause encontrar os butoes
            pauseBtn = GameObject.Find("Btn_Pause").GetComponent<Button>();
            pauseBtn_Return = GameObject.Find("btn_Resume").GetComponent<Button>();
            //JogarNov
            jogarNovBtn = GameObject.Find("Btn_JogarNov").GetComponent<Button>();
            //You Lose encontrar os butoes
            novamenteBtnLose = GameObject.Find("btn_JogarNovamenteLose").GetComponent<Button>();
            levelBtnLose = GameObject.Find("btn_MenuFasesLose").GetComponent<Button>();
            //You Win encontrar os butoes
            novamenteBtnWin = GameObject.Find("btn_JogarNovamenteWin").GetComponent<Button>();
            levelBtnWin = GameObject.Find("btn_MenuFasesWin").GetComponent<Button>();
            avancarBtnWin = GameObject.Find("btn_AvancarWin").GetComponent<Button>();


            //Pausar chamada dos metodos para os butões
            pauseBtn.onClick.AddListener(Pause);//para pausar
            pauseBtn_Return.onClick.AddListener(PauseReturn);//para sair do pause
            //jogarNov
            jogarNovBtn.onClick.AddListener(JogarNovamente);
            //You Lose chamada dos metodos para os butões
            novamenteBtnLose.onClick.AddListener(JogarNovamente);
            levelBtnLose.onClick.AddListener(Levels);
            //You Win chamada dos metodos para os butões
            novamenteBtnWin.onClick.AddListener(JogarNovamente);
            levelBtnWin.onClick.AddListener(Levels);
            avancarBtnWin.onClick.AddListener(ProximaFase);


            moedasNumAntes = PlayerPrefs.GetInt("moedasSave");//quardar a quantidade de moedas do começo da partida
        }
    }

    public void StartUi() {
        LigaDesligaPainel();

    }

    public void UpdateUI() {

        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
    }

    public void GameOverUI() {
        losePainel.SetActive(true);
    }

    public void WinGameUI() {
        winPainel.SetActive(true);
    }

    void Pause() {
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("PNLPAUSE");
        Time.timeScale = 0;//tempo em que a cena esta passando 
        
    }

    void PauseReturn() {
        pausePainel.GetComponent<Animator>().Play("PNLPAUSEINVERSE");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
    }

    IEnumerator EsperaPause() {

        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
    }
    
    void LigaDesligaPainel() {

        StartCoroutine(Tempo());
    }

    IEnumerator Tempo() {

        yield return new WaitForSeconds(0.0001f);
        losePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);

    }

    void JogarNovamente() {
        if (!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == true) {

            SceneManager.LoadScene(OndeEstou.instance.fases);
            AdsUnity.instance.adsBtnAcionado = false;

        }else if(!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == false) {
            SceneManager.LoadScene(OndeEstou.instance.fases);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
        }
        else {
            SceneManager.LoadScene(OndeEstou.instance.fases);
            resultado = 0;
        }
    }

    void Levels() {
        if (!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == true) {
            
            SceneManager.LoadScene("LEVEL_GAME");
            AdsUnity.instance.adsBtnAcionado = false;

        }
        else if (!GameManager.instance.win && AdsUnity.instance.adsBtnAcionado == false) {
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(4);
        }
        else {
            resultado = 0;
            SceneManager.LoadScene("LEVEL_GAME");
        }

    }

    void ProximaFase() {
        if (GameManager.instance.win){
            int temp = OndeEstou.instance.fases + 1;
            SceneManager.LoadScene(temp);

        }

    }

}
