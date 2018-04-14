using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour {

	public GameObject BotaoSair;

	// Use this for initialization
	void Start () {
		#if UNITY_STANDALONE || UNITY_EDITOR
			BotaoSair.SetActive(true);
		#endif
	}



	IEnumerator MudarCena(string nomeDaCena)
	{

		yield return new WaitForSeconds(0.3f);
		SceneManager.LoadScene(nomeDaCena);
	}

	public void JogarJogo()
	{
		MudarCena("cena1");
	}

	IEnumerator SairJogo()
	{
		yield return new WaitForSeconds(0.3f);
		Application.Quit();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
