using UnityEngine;
using System.Collections;

public class GeradorChefe : MonoBehaviour
{
	private float tempoParaProximaGeracao = 0;
	public float TempoEntreGeracoes = 60;
	public GameObject ChefeDeFase;

	private ControlaInterface scriptControlaInterface;

	public Transform[] PosicoesPossiveisChefes;
	public Transform Player;

	// Use this for initialization
	void Start()
	{
		tempoParaProximaGeracao = TempoEntreGeracoes;
		scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
		Player = GameObject.FindWithTag(Tags.Jogador).transform;
	}

	// Update is called once per frame
	void Update()
	{
		if(Time.timeSinceLevelLoad > tempoParaProximaGeracao)
		{
			Vector3 posicaoCriarChefe = PosicaoMaiorDistanciaJogador();
			Instantiate(ChefeDeFase, posicaoCriarChefe, Quaternion.identity);
			scriptControlaInterface.AparecerTextoChefeCriado();
			tempoParaProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
		}
	}

	Vector3 PosicaoMaiorDistanciaJogador()
	{
		Vector3 posicaoMaiorDistancia = Vector3.zero;
		float maiorDistancia = 0;
		foreach(Transform lugares in PosicoesPossiveisChefes)
		{
			float Distancia = Vector3.Distance(lugares.position, Player.position);
			if(Distancia > maiorDistancia)
			{
				maiorDistancia = Distancia;
				posicaoMaiorDistancia = lugares.position;
			}
		}

		return posicaoMaiorDistancia;
	}
}
