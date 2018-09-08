using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BB8 : MonoBehaviour {

    public SimpleHealthBar healthBar;
    public SimpleHealthBar shieldBar;
    public SimpleHealthBar energyBar;
    private int maxHealth = 1000;
    private int maxEnergy = 50;
    private int attackCost = 10;
    private int daño = 1;
    private int maxShield;
    public static int currentHealth;
    private int currentShield;
    private float currentEnergy;

    private Vector3 initPos;

    public GameObject player;
    public GameObject explosion;
    public GameObject rayo;
    public GameObject[] escudo;
    private Rigidbody rigidbody;
    private GameObject plano;

    public GameObject deathMenu;

    private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem m_ParticleSystem;

    void Start()
    {
        plano= GameObject.Find("Plane");
        SumScore.HighScore = PlayerPrefs.GetInt("BB8-HighScore");
        initPos =transform.GetComponentInParent<Vuforia.ImageTargetBehaviour>().transform.position;
        maxShield = maxHealth / 2;
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentEnergy = maxEnergy;
        healthBar.UpdateBar(currentHealth, maxHealth);
        healthBar.UpdateBar(currentShield, maxShield);
        shieldBar.UpdateBar(currentEnergy, maxEnergy);
        m_ParticleSystem = GetComponent<ParticleSystem>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void keepPos()
    {
        transform.position = new Vector3(transform.position.x, plano.transform.position.y+1.5f, transform.position.z);
    }

    public void attack()
    {
        if (currentEnergy > attackCost)
        {
            currentEnergy -= attackCost;
            energyBar.UpdateBar(currentEnergy, maxEnergy);
            rayo.SetActive(true);
            Invoke("apagaRayo", 0.5f);
        }
    }

    void apagaRayo()
    {
        rayo.SetActive(false);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "Flames") {
            if (currentHealth > 0)
            {
                if (currentShield == 0)
                {
                    currentHealth -= daño;
                }
                else
                {
                    currentShield -= daño;
                    if (currentShield < 0)
                    {
                        currentHealth += currentShield;
                    }
                }
            }
        }


    }

    private void Update()
    {
        keepPos();
        healthBar.UpdateBar(currentHealth, maxHealth);
        shieldBar.UpdateBar(currentShield, maxShield);
        energyBar.UpdateBar(currentEnergy, maxEnergy);
        if (currentHealth <= 0)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            Destroy(player);
            PlayerPrefs.SetInt("BB8-HighScore", SumScore.HighScore);
            //SumScore.SaveHighScore();
            SumScore.Reset();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene("menu");
            deathMenu.SetActive(true);
        }

        if (currentEnergy < maxEnergy)
        {
            currentEnergy += 10 * Time.deltaTime;
        }

        if (currentShield <= 0)
        {
            escudo[0].SetActive(false);
            escudo[1].SetActive(false);
        }
        else
        {
            escudo[0].SetActive(true);
            escudo[1].SetActive(true);
        }
    }

}