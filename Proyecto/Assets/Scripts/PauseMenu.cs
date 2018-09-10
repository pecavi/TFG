using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public static bool paused = false;
    public GameObject UI;

    public GameObject pauseMenu;


    // Use this for initialization
    void Start () {
        pauseMenu.gameObject.SetActive(false);
        //UI= GameObject.Find("UI");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;

        }
        UI.SetActive(!paused);
        pauseMenu.gameObject.SetActive(paused);
        
    }

    public void Exit()
    {
        SceneManager.LoadScene("menu");
        paused = false;
    }
}
