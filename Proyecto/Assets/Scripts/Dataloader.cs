using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dataloader : MonoBehaviour {

	public Text datosPantalla;
	// Use this for initialization
	IEnumerator Start () {
		WWW itemsData=new WWW("http://192.168.1.9/tfg/itemsdata.php");
		yield return itemsData;
		string itemsDataString=itemsData.text;
		datosPantalla.text=itemsDataString;
	}

}
