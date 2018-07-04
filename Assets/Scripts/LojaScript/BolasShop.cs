using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour {

    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolasSuporteList = new List<GameObject>();
    public List<GameObject> compraBtnList = new List<GameObject>();

    public GameObject baseBolaItem;
    public Transform conteudo;


    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    void Start () {
        FillList();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FillList() {
        foreach(Bolas b in bolasList){
            GameObject itensBolas = Instantiate(baseBolaItem) as GameObject;
            itensBolas.transform.SetParent(conteudo,false);
            BolasSuporte item = itensBolas.GetComponent<BolasSuporte>();

            item.bolaID = b.bolasID;
            item.bolaPreco.text = b.bolasPreco.ToString();
            item.btnComprar.GetComponent<CompraBola>().bolasIDe = b.bolasID;

            //Lista compraBtnList
            compraBtnList.Add(item.btnComprar);

            //Lista bolaSuporteList
            bolasSuporteList.Add(itensBolas);

            if (PlayerPrefs.GetInt("BTN" + item.bolaID) == 1) {
                b.bolasComprou = true;

            }

            if (b.bolasComprou){
                item.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + b.bolasNomeSprite);
                item.bolaPreco.text = "Comprado!";
            }
            else {
                item.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + b.bolasNomeSprite + "_cinza");
            }
        }

    }

    public void UpdateSprite(int bola_id) {
        for (int i = 0; i < bolasSuporteList.Count; i++) {
            BolasSuporte bolasSuporteScript = bolasSuporteList[i].GetComponent<BolasSuporte>();

            if (bolasSuporteScript.bolaID == bola_id) {
                for (int j = 0; bola_id < bolasList.Count; j++) {
                    if (bolasList[j].bolasID == bola_id) {
                        if (bolasList[j].bolasComprou){
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + bolasList[j].bolasNomeSprite);
                            bolasSuporteScript.bolaPreco.text = "Comprado!";
                            SalvaBolasInfo(bolasSuporteScript.bolaID);
                        }
                        else {
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("Sprite/" + bolasList[j].bolasNomeSprite + "_cinza");
                        }
                    }
                }

            }
        }
    }

    void SalvaBolasInfo(int idBolas) {
        for (int i = 0; i < bolasList.Count; i++) {
            BolasSuporte bolasSup = bolasSuporteList[i].GetComponent<BolasSuporte>();

            if (bolasSup.bolaID == idBolas) {
                PlayerPrefs.SetInt("BTN" + bolasSup.bolaID, bolasSup.btnComprar ? 1 : 0);
            }
        }

    }
}
