using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour {

    private Animator infoAnim;
    private AudioSource musica;
    //            somLigado  somDesligado
    public Sprite somOn, somOf;
    private Button btnSom;

    void Start()
    {
        infoAnim = GameObject.FindGameObjectWithTag("MenuInfo").GetComponent<Animator>() as Animator;
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSom = GameObject.Find("SOM").GetComponent<Button>() as Button;
    }

    public void AnimaInfo() {

        infoAnim.Play("InfAnim");
    }

    public void FechaInfo() {
        
        infoAnim.Play("InfAnimInverse");

    }

    public void LigaDesligaSom() {
        musica.mute = !musica.mute;

        if (musica.mute)
        {
            btnSom.image.sprite = somOf;
        }
        else {
            btnSom.image.sprite = somOn;
        }

    }

    public void Facebook() {
        Application.OpenURL("www.xvideos.com/");
    }
}
