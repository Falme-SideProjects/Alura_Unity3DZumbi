using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour {

    public ControlaJogador Jogador;
    public Slider SliderVidaJogador;
    public GameObject panelGameOver;
    public Text textGameOver;

    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos = 0;
    public Text TextoNumeroZumbisMortos, TextoChefeAparece;



    // Use this for initialization
    void Start () {
        Jogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = Jogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();

        Time.timeScale = 1;

        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = Jogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int) Time.timeSinceLevelLoad / 60;
        int segundos = (int) Time.timeSinceLevelLoad % 60;

        textGameOver.text = "você sobreviveu por "+minutos+"m e "+segundos+"s";

        AjustarPontuacaoMaxima(minutos, segundos);

    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor jogo é de {0}min e {1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if (TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);
        }

    }

	IEnumerator Restart()
    {
		yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene("cena1");
    }

    public void AtualizarQuantidadeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoNumeroZumbisMortos.text = string.Format("x{0}", quantidadeDeZumbisMortos);
    }

	public void AparecerTextoChefeCriado()
	{
		StartCoroutine(DesaparecerTexto(2f, TextoChefeAparece));
	}

	IEnumerator DesaparecerTexto(float TempoParaSumir, Text textoParaSumir)
	{

		textoParaSumir.gameObject.SetActive(true);
		Color corTexto = textoParaSumir.color;
		corTexto.a = 1;
		textoParaSumir.color = corTexto;
		yield return new WaitForSeconds(1);
		float contador = 0;
		while(textoParaSumir.color.a > 0)
		{
			contador += (Time.deltaTime / TempoParaSumir);
			corTexto.a = Mathf.Lerp(1, 0, contador);
			textoParaSumir.color = corTexto;
			if(textoParaSumir.color.a <= 0)
				textoParaSumir.gameObject.SetActive(false);
			yield return null;

		}
	}
}
