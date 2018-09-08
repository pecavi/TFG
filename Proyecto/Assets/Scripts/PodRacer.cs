using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class PodRacer : Photon.MonoBehaviour, IPunObservable {

    private Slider sliderLeft;
    private Slider sliderRight;
    private GameObject target;
    private GameObject meta;

    public int lap = 0;

    private bool[] flags;

    public Transform[] gravityPoints;
    private Rigidbody body;
    public float hoverForce=2f;
    public float hoverHeight;
    public float speed = 1f;
    private SimpleTouchController leftController;
    //public Transform COM;

    // Use this for initialization
    void Start () {

        flags = new bool[4];
        leftController = GameObject.FindObjectOfType<SimpleTouchController>();
        meta = GameObject.Find("A");
        transform.position = meta.transform.position;
        body = GetComponent<Rigidbody>();
        //body.centerOfMass = COM.position;
        //Turn off Vuforia
        //VuforiaBehaviour.Instance.enabled = false;

    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(isLocalPlayer);
       /* if (!isLocalPlayer)
            return;*/
        if(photonView.isMine){

            float x = leftController.GetTouchPosition.x;
            float y = leftController.GetTouchPosition.y;
            if(Mathf.Atan2(x, y)!=0)
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(x, y) * Mathf.Rad2Deg + 90,0);
        }
    }

    private void FixedUpdate()
    {
        if(photonView.isMine){
        /* if (!isLocalPlayer)
                return;*/
            float x = leftController.GetTouchPosition.x;
            float y = leftController.GetTouchPosition.y;
            var direction = new Vector3(x, 0, y);
            var rotation = new Vector3(y, 0, -x);
            body.AddForce(transform.forward * direction.magnitude*speed);
            rotation *= Time.deltaTime * speed * (2 * Mathf.PI * transform.localScale.magnitude) * 2;
            // and rotate by the rotation
            //transform.rotation = Quaternion.Euler(270, 180, Mathf.Atan2(x, y) * Mathf.Rad2Deg + 90);
            
                //transform.eulerAngles = new Vector3(0, Mathf.Atan2(x,y) * 180 / Mathf.PI, 0);
            // transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);


            hover3();
        }

        
    }

    void Hover1()
    {
        Transform hoverPoint;
        // body.AddForceAtPosition(hoverPoint.transform.up * (hoverForce-50), hoverPoint.transform.position);
        RaycastHit hit;
        for (int i = 0; i < gravityPoints.Length; i++)
        {
            hoverPoint = gravityPoints[i];

            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight))
            {
                float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight; ;// 1f -(hit.distance) / hoverHeight;
                Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;

                body.AddForce(appliedHoverForce, ForceMode.Acceleration);
                body.AddForceAtPosition(appliedHoverForce, hoverPoint.transform.position, ForceMode.Acceleration);
                

            }

            
        


        }

        Vector3 impulseForce= body.transform.forward * hoverForce * speed * sliderLeft.value;
        body.AddForceAtPosition(impulseForce, gravityPoints[0].transform.position);

        impulseForce = body.transform.forward * hoverForce * speed * sliderRight.value;
        body.AddForceAtPosition(impulseForce, gravityPoints[1].transform.position);
    }

    void Hover2()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 forceDown = Vector3.down* proportionalHeight * hoverForce;
            Vector3 forceUp = Vector3.up * proportionalHeight * hoverForce;
            //body.AddForce(forceDown, ForceMode.Acceleration);
            body.AddForce(forceUp, ForceMode.Acceleration);
        }
        for (int i = 0; i < gravityPoints.Length; i++) { 
            Vector3 impulseForce = gravityPoints[i].transform.forward * hoverForce * speed;
        }
        //body.AddRelativeForce(0f, 0f, powerInput * speed);
        //body.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

    }

    void hover3()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 forceDown = Vector3.down * proportionalHeight * hoverForce;
            Vector3 forceUp = Vector3.up * proportionalHeight * hoverForce;
            //body.AddForce(forceDown, ForceMode.Acceleration);
            body.AddForce(forceUp, ForceMode.Acceleration);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("A"))
        {
            if (lap == 0)
            {
                Debug.Log(++lap);
                flags[0] = true;
            }
            else if (flags[3])
            {
                flags = new bool[4];
                flags[0] = true;
                Debug.Log(++lap);
            }
        }
        else if (other.name.Equals("B"))
        {
            if (flags[0])
            {
                flags[1] = true;
            }
        }
        else if (other.name.Equals("C"))
        {
            if (flags[1])
            {
                flags[2] = true;
            }
        }
        else if (other.name.Equals("D"))
        {
            if (flags[3])
            {
                flags[3] = false;
            }
            else if (flags[2])
            {
                flags[3] = true;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
