using UnityEngine;
using System.Collections;

public class ShotBehaviorTraining : MonoBehaviour {

    public int speed=10;
    private bool impact = false;

	// Use this for initialization
	void Start () {

        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position += transform.forward * speed*0.1f;
        if (!impact)
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        impact = true;
    }
}
