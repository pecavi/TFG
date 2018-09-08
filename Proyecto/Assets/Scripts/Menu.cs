using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using Vuforia;

public class Menu : MonoBehaviour {

    bool destroyed=false;

    Text labelLog;

	// Use this for initialization
	IEnumerator Start () {
		XRSettings.enabled = false;
        yield return new WaitUntil(() => gameObject.transform.Find("Login")!=null);

        GameObject butLog=gameObject.transform.Find("Login").gameObject;
        
        labelLog=butLog.GetComponentInChildren<Text>();
        if(logIn.logged){
            labelLog.text="STATS";
        }else{
            labelLog.text="SIGN-IN";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(!destroyed)
		   // Destroy (GameObject.Find("TextureBufferCamera"));
        destroyed=true;
	}

    public void loadBB8()
    {
        SceneManager.LoadScene("BB8");
    }

    public void loadXwing()
    {

        SceneManager.LoadScene("turret");
    }

    public void loadDiorama()
    {

        SceneManager.LoadScene("diorama");
    }

    public void loadSignIn(){
        if(logIn.logged){
            SceneManager.LoadScene("stats");
        }else{
            SceneManager.LoadScene("sign-in");
        }
        
    }

    public void loadLogin()
    {

        SceneManager.LoadScene("login");
    }

    public void loadReg()
    {

        SceneManager.LoadScene("regUser");
    }

    public void backMenu(){
        SceneManager.LoadScene("menu");
    }

    public void exit()
    {
        Application.Quit();
    }
}
