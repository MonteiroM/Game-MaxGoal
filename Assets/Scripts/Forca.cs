using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forca : MonoBehaviour {

	private Rigidbody2D bola;
	private float force = 0f;
	private Rotacao rotacao;
	[SerializeField]private Image seta2Img;


	void Start () {
		bola = GetComponent<Rigidbody2D> ();
		rotacao = GetComponent<Rotacao> ();
	}
	
	// Update is called once per frame
	void Update () {
		AplicaForca ();
		ControlaForca ();
	}

	void AplicaForca(){
		float x = force * Mathf.Cos (rotacao.zRotate * Mathf.Deg2Rad);
		float y = force * Mathf.Sin (rotacao.zRotate * Mathf.Deg2Rad);


		if(rotacao.liberaTiro){
			bola.AddForce (new Vector2 (x, y));
			rotacao.liberaTiro = false;
		}
	}

	void ControlaForca(){
		if (rotacao.liberaRotate) {
			float moveX = Input.GetAxis ("Mouse X");

			if (moveX < 0) {
				seta2Img.fillAmount += 1 * Time.deltaTime;
				force = seta2Img.fillAmount * 1000;
			}

			if (moveX > 0) {
				seta2Img.fillAmount -= 1 * Time.deltaTime;
				force = seta2Img.fillAmount * 1000;
			}
		}
	}
}
