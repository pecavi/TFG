using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegUser : MonoBehaviour {

	public Text inputUserName;
	public Text inputPassword;
	public Text inputRePassword;
	public Text inputEmail;

	bool chk=false;
	

	bool registered=false;

	public GameObject errorWindow;

    public Button regButton;

	string createUserURL="http://localhost/tfg/insertuser.php";

	string getUsersEmailURL="http://localhost/tfg/itemsData.php";

	string message;
    private Text errorsText;


	private List<string> emails;

	private List<string> users;

	private string datos;

    IEnumerator Start(){
		errorsText=errorWindow.GetComponentInChildren<Text>();
        errorWindow.SetActive(false);
		emails=new List<string>();
		users=new List<string>();

		WWW itemsData=new WWW(getUsersEmailURL);
		yield return itemsData;
		string itemsDataString=itemsData.text;
		datos=itemsDataString;
		//Debug.Log(datos);
    }

	void validate(){

		string[] data=datos.Split('\n');
		foreach(string s in data){
			//Debug.Log(s);
			if(!s.Equals("")){
				users.Add(s.Split(':')[0]);
				emails.Add(s.Split(':')[1]);
			}
		}

		message="";

		validateUser();
		validateEmail();

		validatePassword();
	}

	void validateUser(){
		if(inputUserName.text.Equals("")){
			message+="Nombre de usuario obligatorio\n";
		}else if(users.Contains(inputUserName.text)){
			message+="Nombre de usuario no disponible\n";
		}
	}

	bool validatePassword(){
		bool ok=true;

		if(inputPassword.text.Equals("")){
			message+="Contraseña obligatoria\n";
			ok=false;
		}
		
		if(inputPassword.text.Length<8){
			message+="La contraseña debe tener 8 caracteres\n";
			ok=false;
		}
		if(!formatPassword()){
			message+="La contraseña debe contener números\n";
			ok=false;
		}
		if(!inputPassword.text.Equals(inputRePassword.text)){
			message+="Las contraseñas no coinciden\n";
			ok=false;
		}


		return ok;
	}

	bool formatPassword(){
		bool ok=false;

		string number="0123456789";

		foreach(char c in number){
			if(inputPassword.text.Contains(c.ToString()))
				ok=true;
		}

		return ok;
	}

	// Use this for initialization
	public void RegisterUser () {
		validate();
		if(message.Equals("")){
			StartCoroutine( CreateUser(inputUserName.text,inputPassword.text,inputEmail.text));  
		}
		else{
			Text errorsText=errorWindow.GetComponentInChildren<Text>();
			errorsText.text=message;
            showErrorPanel(true);
        }
	}

    public void showErrorPanel(bool b){
        regButton.gameObject.SetActive(!b);
        errorWindow.SetActive(b);
		errorsText.text=message.Equals("")?"Usuario registrado":message;
		if(message.Equals("")){
			registered=true;
			logIn.logged=true;
			logIn.user=inputUserName.text;
		}
    }

	IEnumerator CreateUser(string username, string password,string email){
		WWWForm form=new WWWForm();
		form.AddField("usernamePost",username);
		form.AddField("passwordPost",password);
		form.AddField("emailPost",email);
		WWW www=new WWW(createUserURL,form);
		yield return www;
		showErrorPanel(true);
	}

	public void Exit(){
        SceneManager.LoadScene("sign-in");
    }

	public void Login()
    {
		if(registered)
        	SceneManager.LoadScene("login");
		else
			errorWindow.SetActive(false);
    }

	void validateEmail()
	{
		
		if(inputEmail.text.Equals("")){
			message+="Email obligatorio\n";
		}else{
			
			string email=inputEmail.text;
			try {
				Regex rx = new Regex(
			@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
				bool v=rx.IsMatch(email);
				if(!v)
					message+="Email no válido\n";
			}
			catch {
				message+="Email no válido\n";
			}

			if(emails.Contains(email)){
				message+="Email ya usado\n";
			}
		}
	}
}
