using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "Rayo")
        {
            Debug.Log("rayo");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "RayoCollider")
        {
            Debug.Log("rayo");
        }

    }
}
