using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class EnemyManager : MonoBehaviour {

    public float spawnTime = 20f;            // How long between each spawn.
    public Transform[] spawnPoints;
    public GameObject enemy;
    
    private ImageTargetBehaviour target;
    private int maxEnemys = 5;
    public static int cont = 0;

    public Text hScore;


    // Use this for initialization
    void Start () {
        cont=0;
        hScore.text=Usuario.maxScore2.ToString();
        Time.timeScale = 1;
        target = GetComponentInParent<ImageTargetBehaviour>();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Update()
    {
        //Physics.gravity = new Vector3(0, -1.0F, 0);
    }

    




    void Spawn()
    {
        // If the player has no health left...
        if (BB8.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }
        if (cont < maxEnemys) {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject enemyClone=Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            enemyClone.transform.parent = target.gameObject.transform;
            enemyClone.transform.localScale = enemy.transform.localScale;
            cont++;
        }
    }
}
