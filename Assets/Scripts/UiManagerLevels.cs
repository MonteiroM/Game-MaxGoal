using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManagerLevels : MonoBehaviour {
    [SerializeField]
    private Text moedasLevels;

	// Use this for initialization
	void Start () {
       // moedasLevels = GameObject.Find("PontosUi2").GetComponent<Text>();
        moedasLevels.text = PlayerPrefs.GetInt("moedasSave").ToString();
	}
	
}
