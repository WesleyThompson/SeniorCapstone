using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(Collider))]
public class DetectHazards : MonoBehaviour {

	Collider col;
	public Respawn getThisScript;
	// Use this for initialization
	void Start () {
		col = GetComponent<Collider> ();

		if(col.isTrigger == false){
			Debug.LogError ("Error. No Trigger.");
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
			print ("Player entered trigger "+other.gameObject.name);

			try{
				other.gameObject.GetComponent<Respawn> ().RespawnPlayer();
				//getThisScript.RespawnPlayer();
			}
			catch (Exception e){
				Debug.Log (e.Message);
			}
			Debug.Log ("problem");

			if (getThisScript == null) {
				Debug.Log ("no script");
			} else {
				Debug.Log ("what is wrong");
			}


		}

	}

	void OnTriggerExit(Collider Other){
		if(Other.gameObject.tag == "Player"){
			print("Player exited the trigger");

		}
	}

	// Update is called once per frame
	void Update () {

	}
}
