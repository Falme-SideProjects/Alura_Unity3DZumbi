using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {
    
    private Vector3 vector;

    public LayerMask mascaraChao;
    public GameObject textGameover;
    

    public ControlaInterface controlaInterface;
    public AudioClip som;

    private AnimacaoPersonagem animacaoJogador;
    private MovimentoJogador meuMovimentoJogador;
    public Status statusJogador;

    void Start()
    {

        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();


    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        vector = new Vector3(eixoX, 0f, eixoZ);

        // transform.Translate(vector * velocidade * Time.deltaTime);

        animacaoJogador.Movimentar(vector.magnitude);

       

    }


    void FixedUpdate () {

        meuMovimentoJogador.Movimentar(vector, statusJogador.Velocidade);

        meuMovimentoJogador.RotacionarJogador(mascaraChao);

    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        controlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(som);

        if (statusJogador.Vida <= 0)
        {
            Matar();
        }
    }

    public void Matar()
    {

        controlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        statusJogador.Vida = Mathf.Clamp(statusJogador.Vida, 0, statusJogador.VidaInicial);
        //if(statusJogador.Vida > 100) { }
        controlaInterface.AtualizarSliderVidaJogador();
    }
}
