using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    public float Velocidade = 20f;
    public AudioClip som;
    public int danoBala = 1;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
		Quaternion balaRotacaoNegativa = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag)
        {
			case Tags.Inimigo :
				ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
				inimigo.TomarDano(danoBala);
				inimigo.ParticulaSangue(transform.position, balaRotacaoNegativa);
			break;
			case Tags.ChefeDeFase:
				ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
				chefe.TomarDano(danoBala);
				chefe.ParticulaSangue(transform.position, balaRotacaoNegativa);
			break;
		}

        Destroy(gameObject);
        
    }
    
}
