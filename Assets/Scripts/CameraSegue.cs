using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour {
    
    [SerializeField]
    private Transform objE, objD, bola;
    private float t = 1;
	
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.jogoComecou) {

            if (transform.position.x > objE.position.x) {

                t -= 0.8f * Time.deltaTime;
                transform.position = new Vector3(Mathf.SmoothStep(objE.position.x, Camera.main.transform.position.x, t),this.transform.position.y,this.transform.position.z);
            }

            if (bola == null && GameManager.instance.bolasEmCena > 0) {
                bola = GameObject.Find("Bola(Clone)").GetComponent<Transform>();

            } else if (GameManager.instance.bolasEmCena > 0) {
                Vector3 posCamera = transform.position;
                posCamera.x = bola.position.x;
                posCamera.x = Mathf.Clamp(posCamera.x, objE.position.x, objD.position.x);
                transform.position = posCamera;
            }
        }
		
	}
}
