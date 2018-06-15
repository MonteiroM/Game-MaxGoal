using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rotacao : MonoBehaviour {

	//posição seta
	[SerializeField]private Transform posStart;
	//seta
	[SerializeField]private Image setaImg;
	//angulo
	public float zRotate;
	public bool liberaRotate = false;
	public bool liberaTiro = false;

	void Start () {
		PosicionaSeta ();
		PosicionaBola ();
	}
	
	// Update is called once per frame
	void Update () {
		RotacaoSeta ();
		LimitaRotacao ();
		InputDeRotacao ();
	}

	void PosicionaSeta(){
		setaImg.rectTransform.position = posStart.position;
	}

	void PosicionaBola(){
		this.gameObject.transform.position = posStart.position;
	}

	void RotacaoSeta(){
		setaImg.rectTransform.eulerAngles = new Vector3 (0, 0, zRotate);
	}

	void InputDeRotacao(){

		if(liberaRotate){
			
			float moveY = Input.GetAxis ("Mouse Y");

			if (zRotate < 90) {
				if (moveY > 0) {
					zRotate += 2.5f;
				}
			}

			if (zRotate > 0) {
				if (moveY < 0) {
					zRotate -= 2.5f;
				}
			}
		} 
	}

	void LimitaRotacao(){
		if (zRotate >= 90) {
			zRotate = 90;
		}

		if (zRotate <= 0) {
			zRotate = 0;
		}
	}

	void OnMouseDown(){
		liberaRotate = true;
	}

	void OnMouseUp(){
		liberaRotate = false;
		liberaTiro = true;
	}
}
