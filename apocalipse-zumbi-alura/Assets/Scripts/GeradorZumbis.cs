using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject zumbi;
    private float temporizador = 0;
    public float tempoGeradorZumbi;
    public LayerMask layerZumbi;
    private float distanceSpawn = 3;

    private float spawnDistanceJogador = 20;
    private GameObject jogador;

	private int numeroMaximoDeZumbis = 2;
	private int numeroAtualDeZumbis;

	private float tempoProximoAumentoDeDificuldade = 5;
	private float contadorDeAumentarDificuldade = 0;

	void Start()
    {
        jogador = GameObject.FindWithTag(Tags.Jogador);
		contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
		for (int a=0; a<numeroMaximoDeZumbis; a++)
		{
			StartCoroutine(GerarNovoZumbi());
		}
    }

    // Update is called once per frame
    void Update () {

		bool possoGerarZumbi = Vector3.Distance(transform.position, jogador.transform.position) > spawnDistanceJogador;


		if (possoGerarZumbi && numeroAtualDeZumbis < numeroMaximoDeZumbis)
        {

            temporizador += Time.deltaTime;
            if (temporizador >= tempoGeradorZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                temporizador = 0;
            }
        }

		if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
		{
			numeroMaximoDeZumbis++;
			contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
		}
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceSpawn);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicao = aleatorizaPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicao, 1, layerZumbi);

        while(colisores.Length > 0)
        {
            posicao = aleatorizaPosicao();
            colisores = Physics.OverlapSphere(posicao, 1, layerZumbi);
            yield return null;
        }

        ControlaInimigo zumbiCI = Instantiate(zumbi, posicao, transform.rotation).GetComponent<ControlaInimigo>();
		zumbiCI.meuGerador = this;
		numeroAtualDeZumbis++;

	}

    Vector3 aleatorizaPosicao()
    {
        Vector3 vetor = Random.insideUnitSphere * distanceSpawn;
        vetor += transform.position;
        vetor.y = 0;
        return vetor;
    }

	public void removeZumbi()
	{
		numeroAtualDeZumbis--;
	}
}
