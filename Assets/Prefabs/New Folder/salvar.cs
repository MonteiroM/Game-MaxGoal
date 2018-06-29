using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class salvar : MonoBehaviour {

    public Text txt;
    public InputField caixaTxt;
    private float testeF;
    private int testeI;
    private String testeS;

    // Use this for initialization
    void Start () {
        txt.text = PlayerPrefs.GetInt("IntTeste").ToString();
        //txt.text = PlayerPrefs.GetString("StringTeste");
        //txt.text = PlayerPrefs.GetFloat("FloatTeste").ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SalvarFloat() {

        try { 


            testeI = int.Parse(caixaTxt.text);
            PlayerPrefs.SetInt("IntTeste", testeI);
            //testeS = caixaTxt.text;
            //PlayerPrefs.SetString("StringTeste", testeS);
            //testeF = float.Parse(caixaTxt.text);
            //PlayerPrefs.SetFloat("FloatTeste", testeF);
        }catch(Exception ex)
        {
            print("error"+ex.Message);
        }
        
    }
}
