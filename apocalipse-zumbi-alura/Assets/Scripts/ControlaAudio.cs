using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour {

    private AudioSource audioSource;
    public static AudioSource instancia;

	// Use this for initialization
	void Awake () {
        audioSource = GetComponent<AudioSource>();
        instancia = audioSource;

    }
	
}
