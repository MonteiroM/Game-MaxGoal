using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //Músicas
    public AudioClip[] clips;
    public AudioSource musicBG;
    //sonsFX
    public AudioClip[] clipsFX;
    public AudioSource sonsFX;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }


    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!musicBG.isPlaying) {
            musicBG.clip = GetRandom();
            musicBG.Play();
        }

	}

    AudioClip GetRandom() {

        return clips[Random.Range(0, clips.Length)];
    }

    public void PlaySonsFX(int index) {
        sonsFX.clip = clipsFX[index];
        sonsFX.Play();
    }

}
