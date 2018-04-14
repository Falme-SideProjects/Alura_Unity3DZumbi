using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    [SerializeField]
    private Transform Player;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoPersonagem;
    public AudioClip audioMorteZumbi;
    public Status statusInimigo;
    Vector3 posicaoAleatoria, direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float probabilidadeCairKit = 0.25f;
    public GameObject KitMedico;
    private ControlaInterface scriptControlaInterface;
	[HideInInspector]
	public GeradorZumbis meuGerador;
	public GameObject ParticulaSangueZumbi;


    // Use this for initialization
    void Start () {
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();

        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        Player = GameObject.FindWithTag(Tags.Jogador).transform;

        randomizaZumbi();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(Player.transform.position, transform.position);

        animacaoPersonagem.Movimentar(direcao.magnitude);

        if (distancia > 15f)
        {

            Vagar();
        }
        else if (distancia > 2.5f)
        {
            direcao = Player.position - transform.position;
            movimentoInimigo.Rotacionar(direcao);
            movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoPersonagem.Atacar(false);

        }
        else
        {
            direcao = Player.position - transform.position;
            movimentoInimigo.Rotacionar(direcao);
            animacaoPersonagem.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0)
        {

            posicaoAleatoria = aleatorizaPosicao();
            contadorVagar = tempoEntrePosicoesAleatorias + (Random.Range(-1f,1f));

        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (!ficouPertoOSuficiente)
        {

            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            movimentoInimigo.Rotacionar(direcao);
        }

    }

    Vector3 aleatorizaPosicao()
    {
        Vector3 vetor = Random.insideUnitSphere * 10;
        vetor += transform.position;
        vetor.y = transform.position.y;
        return vetor;
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
        Player.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    private void randomizaZumbi()
    {

        int numeroAleatorio = Random.Range(1, transform.childCount);
        transform.GetChild(numeroAleatorio).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
        {
            Matar();
        }
    }

    public void Matar()
    {
		animacaoPersonagem.Morrer();
		movimentoInimigo.Morrer();
		this.enabled = false;
		Destroy(gameObject, 2);
		scriptControlaInterface.AtualizarQuantidadeZumbisMortos();
        VerificarGeracaoKitMedico(probabilidadeCairKit);
        ControlaAudio.instancia.PlayOneShot(audioMorteZumbi);
		meuGerador.removeZumbi();

	}

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }

	public void ParticulaSangue(Vector3 pos, Quaternion rot)
	{
		Instantiate(ParticulaSangueZumbi, pos, rot);
	}
}
