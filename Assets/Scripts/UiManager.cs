using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public static UiManager instance;
    public Text pontosUI;

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

    void Carrega(Scene cena, LoadSceneMode modo) {

        pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();

    }

    public void UpdateUI() {

        pontosUI.text = ScoreManager.instance.moedas.ToString();
    }
}
