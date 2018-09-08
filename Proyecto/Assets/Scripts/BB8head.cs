using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB8head : MonoBehaviour {

	public SimpleTouchController leftController;
	public GameObject sphere;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = sphere.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = leftController.GetTouchPosition.x;
		float y = leftController.GetTouchPosition.y;


		if (x != 0 && y != 0) {
			rb.transform.localEulerAngles = new Vector3 (0, Mathf.Atan2 (x, y) * Mathf.Rad2Deg - 90, 0);
			transform.localEulerAngles = new Vector3 (240, -90, 90);
		} else {
			transform.localEulerAngles = new Vector3 (270, 0, 0);
		}
			
	}
}
