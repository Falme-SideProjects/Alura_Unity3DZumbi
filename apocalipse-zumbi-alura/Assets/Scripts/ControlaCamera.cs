using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {

    public GameObject Jogador;

    private Vector3 compensar;

	// Use this for initialization
	void Start () {
        compensar = transform.position - Jogador.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Jogador.transform.position + compensar;
	}
}
