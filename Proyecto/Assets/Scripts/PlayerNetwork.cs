using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {


	//[SerializeField] private MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

    GameObject parent;

	// Use this for initialization
	void Start () {
        //parent = transform.Find("ImageTarget").gameObject;
//        transform.parent=parent.transform;
		photonView=GetComponent<PhotonView>();

		
	}
	
	 public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		/*Debug.Log("w "+stream.isWriting);
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(velocity);

        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            //velocity = (Vector3)stream.ReceiveNext();

			Debug.Log(stream.ReceiveNext().ToString());

            float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
            //transform.position += (velocity * lag);
            //print(_netPos);
            //print(velocity);
            //print(PhotonNetwork.time - info.timestamp);

        }*/
    }

	
}
