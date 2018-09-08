using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    public int speed=1000;

	// Use this for initialization
	void Start () {

        Destroy(gameObject, 1);
    }
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed*0.1f;
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name+"-Impacto-"+other.name);
        if (name.Contains("TIEShot") && other.gameObject.name == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Impacto1");
        }
        else if (name.Contains("disparo") && other.gameObject.name.Contains("TIE"))
        {
            Destroy(gameObject);
            Debug.Log("Impacto2");
        }
    }
}
