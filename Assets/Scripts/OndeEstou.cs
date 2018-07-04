using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour {

    public static OndeEstou instance;

    public int fases =-1;
    [SerializeField]
    private GameObject uiManager, gameManager;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += VerificaCena;
    }

    void VerificaCena(Scene cena, LoadSceneMode modo) {
        fases = SceneManager.GetActiveScene().buildIndex;

        if (fases != 4 && fases != 5) {
            Instantiate(uiManager);
            Instantiate(gameManager);
        }

    }

}
