using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private string attackClip = "PA_WarriorAttack_Clip";
    private string stopClip = "PA_WarriorStop_Clip";
    private string deathClip = "PA_WarriorDeath_Clip";

    public SimpleHealthBar healthBar;
    public int maxHealth = 100;
    private int currentHealth;

    private GameObject explosion;

    private GameObject objetivo;

    private ParticleSystem arma;
    public float speed=0.1f;
    public float rotationSpeed;

    public float d;
    private bool attacked;
    private bool destroyed;

    private GameObject plano;

    Vector3 tarPos;

    private Animator anim;
    // Use this for initialization
    void Start () {
        //tarPos = transform.GetComponentInParent<Vuforia.ImageTargetBehaviour>().gameObject.transform.position;
        plano = GameObject.Find("Plane");
        arma = GetSystem("Flames"); ;
        explosion = transform.Find("EnemyDeadExplosion").gameObject;
        objetivo= GameObject.Find("SphereBB8");
        currentHealth = maxHealth;
        //transform.position = new Vector3(transform.position.x, objetivo.transform.position.y-1, transform.position.z);
        arma.Stop();
        rotationSpeed = 0.5f;
        speed = 0.1f; 
        anim = GetComponentInParent<Animator>();


        destroyed = false;
    }

    void keepPos()
    {
        transform.position = new Vector3(transform.position.x, plano.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update () {

        keepPos();

        if (!destroyed && Time.timeScale == 1)
        {
            healthBar.UpdateBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                arma.Stop();
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Collider>().isTrigger = true;
                SumScore.Add(100);
                destroyed = true;
                explosion.transform.position = transform.position;
                explosion.SetActive(true);
                
                anim.SetBool("attack", false);
                anim.SetBool("enemyDead", true);
                Invoke("destroy", 3);

            }
            else
            {
                if (objetivo != null)
                {
                    
                    if (distancia(transform.position, objetivo.transform.position) > d)
                    {

                        anim.SetBool("attack", false);
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(attackClip))
                        {
                            arma.Stop();
                            //rotate to look at the player
                            var lookPos = objetivo.transform.position - transform.position;
                            var rotation = Quaternion.LookRotation(lookPos);
                            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);


                            //move towards the player
                            transform.position = transform.position + transform.forward* (speed  ) ;
                        }

                    }
                    else
                    {


                        anim.SetBool("attack", true);
                        //arma.time = 0;
                        Invoke("attack", 1);

                    }

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackClip))
                    {
                        //rotate to look at the player
                        var lookPos = objetivo.transform.position - transform.position;
                        var rotation = Quaternion.LookRotation(lookPos);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

                    }
                }
                else
                {
                    arma.Stop();
                    anim.SetBool("attack", false);
                    anim.SetBool("playerDead", true);
                    
                }
            }
        }
        else
        {
            arma.Stop();
        }
    }

    private float distancia(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p1.x-p2.x, 2)+ Mathf.Pow(p1.z - p2.z, 2));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "RayoCollider")
        {
            currentHealth -= 30;

            Debug.Log("Vida enemigo: "+currentHealth);
        }

    }

    void destroy()
    {
        EnemyManager.cont--;
        Destroy(gameObject);

    }

    void attack()
    {
        arma.Play();
    }

    private ParticleSystem GetSystem(string systemName)
    {
        Component[] children = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem childParticleSystem in children)
        {
            if (childParticleSystem.name.Equals(systemName))
            {
                return childParticleSystem;
            }
        }
        return null;
    }

}
