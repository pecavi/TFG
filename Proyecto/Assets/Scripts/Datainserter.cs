using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Datainserter : MonoBehaviour {

	public Text inputUserName;
	public Text inputPassword;
	public Text inputRePassword;
	public Text inputEmail;

	public GameObject errorWindow;

	string createUserURL="http://localhost/tfg/insertuser.php";

	private bool validate(){
		return !(inputUserName.text.Equals("") 
				&& inputPassword.text.Equals("") 
				&& inputEmail.text.Equals("")) 
				&& inputPassword.text.Equals(inputRePassword.text);
	}

	// Use this for initialization
	public void RegisterUser () {
		if(validate())
			CreateUser(inputUserName.text,inputPassword.text,inputEmail.text);
		else Debug.Log("Campos incorrectos");
	}

	public void CreateUser(string username, string password,string email){
		WWWForm form=new WWWForm();
		form.AddField("usernamePost",username);
		form.AddField("passwordPost",password);
		form.AddField("emailPost",email);
		WWW www=new WWW(createUserURL,form);
		
	}
}
