using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {

    private int valorDeCura = 15;
    private int tempoDestruir = 5;

    void Start()
    {
        Destroy(gameObject, tempoDestruir);
    }

    private void OnTriggerEnter(Collider objetoColisao)
    {
        if(objetoColisao.tag == Tags.Jogador)
        {
            objetoColisao.GetComponent<ControlaJogador>().CurarVida(valorDeCura);
            Destroy(gameObject);
        }
    }
}
