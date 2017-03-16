using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Penalty Timer:" + PlayerPrefs.GetFloat("penaltyTimer"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
