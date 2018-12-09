using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioMasterScript.intensity.setValue(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioMasterScript.intensity.setValue(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioMasterScript.intensity.setValue(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioMasterScript.intensity.setValue(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioMasterScript.intensity.setValue(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AudioMasterScript.intensity.setValue(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AudioMasterScript.intensity.setValue(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AudioMasterScript.intensity.setValue(10);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioMasterScript.heartbeatVolume.setValue(10);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioMasterScript.heartbeatVolume.setValue(0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioMasterScript.sawPadVolume.setValue(10);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioMasterScript.sawPadVolume.setValue(0);
        }
    }
}
