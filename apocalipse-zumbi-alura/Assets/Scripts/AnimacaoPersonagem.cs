using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Atacar(bool valor)
    {
        anim.SetBool("Atacando", valor);
    }

    public void Movimentar(float valorMovimento)
    {
        anim.SetFloat("Movendo", valorMovimento);
    }

	public void Morrer()
	{
		anim.SetTrigger("Morrer");
	}


}
