using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour {

    public SimpleHealthBar healthBar;
    public SimpleHealthBar shieldBar;
    private int maxHealth = 1000;
    private int daño = 100;
    private int maxShield;
    public static int currentHealth;
    private int currentShield;

    public GameObject pMenu;

    public GameObject deathMenu;

    public GameObject shot;

    // Use this for initialization
    void Start () {
        maxShield = maxHealth / 2;
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.UpdateBar(currentHealth, maxHealth);
        healthBar.UpdateBar(currentShield, maxShield);
    }
	
	// Update is called once per frame
	void Update () {
        healthBar.UpdateBar(currentHealth, maxHealth);
        shieldBar.UpdateBar(currentShield, maxShield);
        if (currentHealth <= 0)
        {
            SumScore.SaveHighScore();
            SumScore.Reset();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Exit();
            deathMenu.SetActive(true);
            Time.timeScale = 0;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "TIEShot(Clone)")
        {
            if (currentShield == 0)
            {
                currentHealth -= daño;
                //Debug.Log("Vida " + currentHealth);
            }
            else
            {
                currentShield -= daño;
                //Debug.Log("Escudo " + currentShield);
            }
        }

    }
    public void shoot()
    {
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.5f);
        Instantiate(shot, transform.position, transform.rotation).SetActive(true);
    }


    void Death(){

    }

    public void Exit()
    {
        SceneManager.LoadScene("menu");
    }

}