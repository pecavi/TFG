using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class PhotonNetworkManager : Photon.MonoBehaviour {

	public GameObject imageTarget;

	GameObject clone;

	static int cloneNumber=1;

	[SerializeField] private Text connectedText;
	[SerializeField] private Text nickname;
	[SerializeField] public GameObject player;

	[SerializeField] public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		MixedRealityController.Instance.SetMode(MixedRealityController.Mode.HANDHELD_AR);
 
		VuforiaARController.Instance.SetWorldCenterMode(VuforiaARController.WorldCenterMode.FIRST_TARGET);
		PhotonNetwork.ConnectUsingSettings("racer");
	}

	public virtual void OnConnectedToMaster(){
		Debug.Log("Connected to master");
	}

	public virtual void OnJoinedLobby(){
		Debug.Log("Joined to lobby");
		PhotonNetwork.JoinOrCreateRoom("New",null,null);
		PhotonNetwork.player.NickName=SystemInfo.deviceName;
	}

	public virtual void OnJoinedRoom(){
		Debug.Log("Joined to room");
		Debug.Log(photonView);
		clone = PhotonNetwork.Instantiate(player.name,spawnPoint.position,Quaternion.identity,0);
		//clone.transform.parent=imageTarget.gameObject.transform;
		//Debug.Log("Clon: "+clone.name);
		PhotonNetwork.player.NickName="Pedro";
		//photonView.RPC("InstantiatePlayer",PhotonTargets.All);
		nickname.text=PhotonNetwork.player.NickName;
	}
	
	// Update is called once per frame
	void Update () {
		
		connectedText.text=PhotonNetwork.connectionStateDetailed.ToString();
	}

	[PunRPC]void InstantiatePlayer(){
		clone = (GameObject)Instantiate(player,spawnPoint.position,spawnPoint.rotation);
		clone.name=clone.name+cloneNumber;
		cloneNumber++;
		Debug.Log("Clon: "+clone.name);
		clone.transform.parent=imageTarget.transform;
	}
}
