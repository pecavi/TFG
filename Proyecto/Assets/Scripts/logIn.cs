using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class logIn : MonoBehaviour {

	public InputField inputPassword;
	public Text inputEmail;

	bool registered=false;

	public GameObject errorWindow;

    public Button regButton;

	string logUserURL="http://localhost/tfg/login.php";

	string getStatsUrl="http://localhost/tfg/getStats.php";

	string message;

	public static string user="";
	public static bool logged=false;

    void Start()
    {
		
        errorWindow.SetActive(false);
    }
	private bool validate(){

		bool ok=true;
		message="";

		if(inputEmail.text.Equals("")){
			ok=false;
			message+="Email obligatorio\n";
		}else{
			ok=validateEmail() && ok;
		}
		if(inputPassword.text.Equals("")){
			ok=false;
			message+="Contraseña obligatoria\n";
		}
		

		return ok;
	}



    public void showErrorPanel(bool b){
        regButton.gameObject.SetActive(!b);
        errorWindow.SetActive(b);
		if(logged){
			backToMenu();
		}
		if(!b){
			
			Text errorsText=errorWindow.GetComponentInChildren<Text>();
			errorsText.text="";
		
		}

    }

	public void logUser () {
		if(validate()){
			StartCoroutine(logUser(inputPassword.text,inputEmail.text));
			Debug.Log("1");
            
		}
		else{
			Text errorsText=errorWindow.GetComponentInChildren<Text>();
			errorsText.text=message;Debug.Log("2");
        }
		showErrorPanel(true);
	}

	IEnumerator logUser(string password,string email){
		WWWForm form=new WWWForm();
		form.AddField("passwordPost",password);
		form.AddField("emailPost",email);
		WWW www=new WWW(logUserURL,form);
		yield return www;
		message=www.text.Equals("Error")?"Datos incorrectos":"Login correcto";
		Text errorsText=errorWindow.GetComponentInChildren<Text>();
		errorsText.text=message;
		if(!www.text.Equals("Error")){
			logged=true;
			user=www.text;
			Usuario.username=user;
			StartCoroutine(getStats());
		}
		
		
		
	}

	public IEnumerator getStats(){
		WWWForm form=new WWWForm();
		form.AddField("player",Usuario.username);
		WWW www=new WWW(getStatsUrl,form);
		yield return www;
		//Debug.Log(www.text);
		string[] scores=www.text.Split(new char[]{'\n'});
		Usuario.maxScore1=long.Parse(scores[0]);
		Usuario.maxScore2=long.Parse(scores[1]);
		Usuario.maxScore3=long.Parse(scores[2]);
	}

	public void Exit()
    {
		Debug.Log(user);
        SceneManager.LoadScene("sign-in");
    }


	public void backToMenu()
    {
		Debug.Log(user);
        SceneManager.LoadScene("menu");
    }

	bool validateEmail()
	{
		string email=inputEmail.text;
		try {
			Regex rx = new Regex(
        @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            bool v=rx.IsMatch(email);
			if(!v)
				message+="Email no válido\n";
			
			return v;
		}
		catch {
			message+="Email no válido\n";
			return false;
		}
	}
}
