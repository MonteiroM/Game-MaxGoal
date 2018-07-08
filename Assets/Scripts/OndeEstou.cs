using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour {

    public static OndeEstou instance;

    public int fases =-1;
    [SerializeField]
    private GameObject uiManager, gameManager;

    public int bolaEmUso;

    private float orthoSize = 5;
    [SerializeField]
    private float aspect = 1.75f;

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
        bolaEmUso = PlayerPrefs.GetInt("BolaUse");
    }

    void VerificaCena(Scene cena, LoadSceneMode modo) {
        fases = SceneManager.GetActiveScene().buildIndex;

        if (fases != 0 && fases != 1 && fases != 2) {
            Instantiate(uiManager);
            Instantiate(gameManager);
            Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }

    }

}
