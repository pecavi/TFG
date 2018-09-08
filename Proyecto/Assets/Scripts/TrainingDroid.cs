using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDroid : MonoBehaviour {

    private GameObject player;
    private GameObject shot;

    // Use this for initialization
    void Start () {
		player= GameObject.Find("Player");
        shot = transform.Find("shotBall").gameObject;
        InvokeRepeating("shoot", 2, 2);
    }
	
	// Update is called once per frame
	void Update () {
        //rotate to look at the target
        var lookPos = player.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

    }

    private void shoot()
    {
        Instantiate(shot, shot.transform.position, transform.rotation).SetActive(true);
    }
}
