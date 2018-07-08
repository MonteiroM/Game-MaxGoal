using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoMenuIniciar : MonoBehaviour {

    private Animator barraAnim;
    private bool sobe;

    public void Jogar() {
        SceneManager.LoadScene("LEVEL_GAME");
    }

    public void AnimaMenu() {

        barraAnim = GameObject.FindGameObjectWithTag("BarraAnimTag").GetComponent<Animator>();

        if (!sobe)
        {
            barraAnim.Play("MOVE_UI");
            sobe = true;
        }
        else {
            barraAnim.Play("MOVE_UI_Inverse");
            sobe = false;
        }
    }

}
