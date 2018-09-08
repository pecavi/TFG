using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class StatsManager : MonoBehaviour {

	public Text maxScore1,maxScore2,maxScore3;

	// Use this for initialization
	void Start () {
		XRSettings.enabled = false;
		//StartCoroutine(getStats());
		maxScore1.text=Usuario.maxScore1.ToString();
		maxScore2.text=Usuario.maxScore2.ToString();
		maxScore3.text=Usuario.maxScore3.ToString();
	}


}
