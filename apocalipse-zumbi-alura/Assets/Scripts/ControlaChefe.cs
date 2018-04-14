using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{

	private Transform playerTransform;
	private NavMeshAgent agentChefe;

	private Status statusChefe;
	private AnimacaoPersonagem animacaoChefe;
	private MovimentoPersonagem movimentaChefe;
	public GameObject KitMedico;
	public Slider sliderVidaChefe;

	public Image imageSliderVida;
	public Color corVidaMinima, corVidaMaxima;
	public GameObject ParticulaSangueZumbi;


	// Use this for initialization
	void Start()
	{
		playerTransform = GameObject.FindWithTag(Tags.Jogador).transform;

		agentChefe = GetComponent<NavMeshAgent>();
		statusChefe = GetComponent<Status>();
		animacaoChefe = GetComponent<AnimacaoPersonagem>();
		movimentaChefe = GetComponent<MovimentoPersonagem>();

		animacaoChefe.Movimentar(agentChefe.velocity.magnitude);
		agentChefe.speed = statusChefe.Velocidade;

		sliderVidaChefe.maxValue = statusChefe.VidaInicial;
		atualizaSliderVida();
	}

	// Update is called once per frame
	void Update()
	{

		agentChefe.SetDestination(playerTransform.position);

		if (agentChefe.hasPath)
		{
			bool estouPertoDoJogador = agentChefe.remainingDistance <= agentChefe.stoppingDistance;
			if (estouPertoDoJogador)
			{
				animacaoChefe.Atacar(true);
				Vector3 direcao = playerTransform.position - transform.position;
				movimentaChefe.Rotacionar(direcao);
			}
			else
			{
				animacaoChefe.Atacar(false);
			}
		}
	}

	void AtacaJogador()
	{
		int dano = Random.Range(30, 50);
		playerTransform.GetComponent<ControlaJogador>().TomarDano(dano);
	}

	public void TomarDano(int dano)
	{
		statusChefe.Vida -= dano;
		atualizaSliderVida();
		if (statusChefe.Vida <= 0)
		{
			Matar();
		}
	}

	public void Matar()
	{
		agentChefe.enabled = false;
		animacaoChefe.Morrer();
		movimentaChefe.Morrer();
		this.enabled = false;
		Instantiate(KitMedico, transform.position, Quaternion.identity);
		Destroy(gameObject, 2);
	}


	public void ParticulaSangue(Vector3 pos, Quaternion rot)
	{
		Instantiate(ParticulaSangueZumbi, pos, rot);
	}

	public void atualizaSliderVida()
	{
		sliderVidaChefe.value = statusChefe.Vida;

		float pocentagemVida = (float) statusChefe.Vida / statusChefe.VidaInicial;
		Color corVidaChefe = Color.Lerp(corVidaMinima, corVidaMaxima, pocentagemVida);
		imageSliderVida.color = corVidaChefe;
	}
}
