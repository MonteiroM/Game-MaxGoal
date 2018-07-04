using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


    [System.Serializable]
    public class Level {

        public string levelTxt;
        public bool habilitado;
        public int desbloqueado;
        public bool txtAtivo;

    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    void ListaAdd() {
        foreach (Level level in levelList) {
            GameObject btnClone = Instantiate(botao) as GameObject;
            BotaoLevel btnNovo = btnClone.GetComponent<BotaoLevel>();

            btnNovo.levelTxtBTN.text = level.levelTxt;

            if (PlayerPrefs.GetInt("Level" + btnNovo.levelTxtBTN.text) == 1) {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }

            btnNovo.desbloqueadoBTN = level.desbloqueado;
            btnNovo.GetComponent<Button>().interactable = level.habilitado;
            btnNovo.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            btnNovo.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + btnNovo.levelTxtBTN.text));

            btnClone.transform.SetParent(localBtn, false);

        }

    }

    void ClickLevel(string level) {

        SceneManager.LoadScene(level);

    }

    void Awake()
    {
        Destroy(GameObject.Find("UiManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    // Use this for initialization
    void Start () {
        
        ListaAdd();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
