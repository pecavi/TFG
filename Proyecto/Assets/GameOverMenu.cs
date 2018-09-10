using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

	string uploadScoreURL="http://localhost/tfg/insertScore.php";

	public Text score;

	public Text achievedScore;

	public int gameMode;

	// Use this for initialization
	void Start () {
		//score.text="2224";
		achievedScore.text="Puntos: "+score.text;
		Debug.Log("Score: "+score.text);
		if(Usuario.getScore(gameMode)<int.Parse(score.text)){
			StartCoroutine(uploadScore());
		}
		
	}
	
	
	IEnumerator uploadScore(){
		Usuario.setScore(gameMode,int.Parse(score.text));
		WWWForm form=new WWWForm();
		form.AddField("player",Usuario.username);
		form.AddField("game",gameMode);
		form.AddField("score",score.text);
		WWW www=new WWW(uploadScoreURL,form);
		yield return www;
		Debug.Log("Upload score: "+www.text);
		if(!www.text.Equals(""))
			Time.timeScale = 0;
	}


	public void reset()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
	}

	public void exit(){
		SceneManager.LoadScene("menu");
	}
}
