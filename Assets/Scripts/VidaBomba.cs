﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBomba : MonoBehaviour {

    private GameObject bombaRep;

	void Start () {
        bombaRep = GameObject.Find("Barril");
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(Vida());
	}

    IEnumerator Vida() {

        yield return new WaitForSeconds(0.5f);
        if (bombaRep != null)
        {
            Destroy(bombaRep.gameObject);
        }
        Destroy(this.gameObject);
    }
}
