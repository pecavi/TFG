using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {

    //public float speed;
    public float rotationSpeed;
    public int d;
    private static GameObject[] respawns;
    private GameObject destino;
    private GameObject anteriorDestino;
    private bool targetChanged;

    private int maxHealth = 200;
    private int currentHealth;
    private GameObject tie;
    private ParticleSystem explosion;
    private GameObject shot;

    void Start()
    {
        tie = transform.Find("TIE_Fighter").gameObject;
        explosion = GetSystem("TIE-Explosion");
        currentHealth = maxHealth;
        InvokeRepeating("shoot", 2, 2);
        targetChanged = false;
        destino = Camera.main.gameObject;
        respawns=null;
        if (respawns == null)
            respawns = GameObject.FindGameObjectsWithTag("SpawnPoint");

        shot= transform.Find("TIEShot").gameObject;
        var lookPos = destino.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    // Update is called once per frame
    void Update () {


        //Debug.Log(transform.position+" - "+destino.transform.position);
        if (currentHealth > 0)
        {
            avanza();

            if (distancia(transform.position, destino.transform.position) <= d)
            {
                if (targetChanged)
                {
                    targetChanged = false;
                    destino = Camera.main.gameObject;
                }
                else
                {
                    anteriorDestino = destino;
                    do
                    {
                        int spawnPointIndex = Random.Range(0, respawns.Length);
                        destino = respawns[spawnPointIndex].gameObject;

                    } while (destino == anteriorDestino);

                    targetChanged = true;
                }
            }

        }

        

    }

    private float distancia(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.z - p2.z, 2));
    }

    private void avanza()
    {
        //rotate to look at the target
        var lookPos = destino.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);


        //move towards the player
        transform.position = transform.position+ transform.forward*  Time.deltaTime;

    }

    private void shoot()
    {
        if(destino.transform.position==Camera.main.transform.position && currentHealth > 0)
            Instantiate(shot, shot.transform.position, transform.rotation).SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "disparo(Clone)")
        {
            
            currentHealth -= 100;
                //Debug.Log("Vida enemigo" + currentHealth);
            if (currentHealth <= 0)
            {

                explosion.Play();
                tie.SetActive(false);
                ShipsManager.contShips--;
                SumScore.Add(100);
                Invoke("Death", 1);
                
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
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
