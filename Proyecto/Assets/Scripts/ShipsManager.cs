using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class ShipsManager : MonoBehaviour {

    public float spawnTime = 20f;            // How long between each spawn.
    public Transform[] spawnPoints;
    public GameObject enemy;
    public int maxShips;
    public static int contShips = 0;

    public Text hScore;

    // Use this for initialization
    void Start()
    {
        contShips = 0;
        hScore.text=Usuario.maxScore1.ToString();
        Time.timeScale = 1;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }





    void Spawn()
    {
        Debug.Log(contShips+"+"+maxShips);
        // If the player has no health left...
        if (contShips == maxShips)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        
        contShips++;
    }
}
