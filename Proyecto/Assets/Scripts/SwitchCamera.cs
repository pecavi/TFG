using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class SwitchCamera : MonoBehaviour {

    public Camera ARcam;
    public Camera Vadercam;
    public Camera Bencam;

	public void ARCam()
    {
        DefaultTrackableEventHandler.render = false;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
        
        ARcam.enabled = true;
        Vadercam.enabled = false;
        Bencam.enabled = false;

    }
    public void VaderCam()
    {
        
        DefaultTrackableEventHandler.render = true;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        ARcam.enabled = false;
        Vadercam.enabled = true;
        Bencam.enabled = false;

    }
    public void BenCam()
    {
        
        DefaultTrackableEventHandler.render = true;
        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        ARcam.enabled = false;
        Vadercam.enabled = false;
        Bencam.enabled = true;

    }

    private void Update()
    {
        Vadercam.transform.rotation = ARcam.transform.rotation;
        Bencam.transform.rotation = ARcam.transform.rotation;
    }


    public void Exit()
    {
        SceneManager.LoadScene("menu");
    }
}
