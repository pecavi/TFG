using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrumpController : MonoBehaviour {

	private Rigidbody rb;
	private Rigidbody rbGyro;
	public GameObject head;
	public GameObject sphere;
	public SimpleTouchController leftController;
    public int speed = 10;
	public int ballSpeed=10;
    public Button butSpeed;


	// Use this for initialization
	void Start () {

        ballSpeed = speed;
		rb = GetComponent<Rigidbody> ();
		rbGyro = sphere.GetComponent<Rigidbody> ();

        EventTrigger trigger = butSpeed.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;

        pointerDown.callback.AddListener((e) => incSpeed());
        pointerUp.callback.AddListener((e) => decSpeed());

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }

    void incSpeed()
    {
        ballSpeed += 7;
    }

    void decSpeed()
    {
        ballSpeed =speed;
    }


    // FixedUpdate is called once per frame
    void FixedUpdate () {
        
		float x = leftController.GetTouchPosition.x;
		float y = leftController.GetTouchPosition.y;


        if (x == 0 && y == 0)
        {

            //rb.AddTorque ( ballSpeed*Time.deltaTime);
            rb.angularVelocity = Vector3.zero;

        }
        else
        {
            rb.AddForce(Vector3.forward * (ballSpeed * y));
            rb.AddForce(Vector3.right * (ballSpeed * x));

            // now collect the movement stuff This is generic direction and rotation.
            var direction = new Vector3(x, 0, y);
            var rotation = new Vector3(y, 0, -x);

            // prevent the ball from moving faster diagnally
            if (direction.magnitude > 1.0) direction.Normalize();
            if (rotation.magnitude > 1.0) rotation.Normalize();


            // multiply the direction by the speed and deltaTime
            direction *= Time.deltaTime * ballSpeed;
            // multiply the rotation by the speed, deltaTime, circumference and 10...
            // dunno why I had to add 10, but it works
            rotation *= Time.deltaTime * ballSpeed * (2 * Mathf.PI * transform.localScale.magnitude) *2;

            // now update the position by the direction
            transform.Translate(direction, Space.World);
            // and rotate by the rotation
            transform.Rotate(rotation, Space.World);
        }
        

        


	}

	// Update is called once per frame
	void Update () {

        float x = leftController.GetTouchPosition.x;
        float y = leftController.GetTouchPosition.y;

        sphere.transform.position = transform.position;
		head.transform.position = transform.position;
        

        if (x != 0 && y != 0) {
			rbGyro.transform.localEulerAngles = new Vector3 (0, Mathf.Atan2 (x, y) * Mathf.Rad2Deg - 90, 0);
			head.transform.localEulerAngles = new Vector3 (240, -90, 90);
		} else {
			head.transform.localEulerAngles = new Vector3 (270, 0, 0);
		}

	}
}