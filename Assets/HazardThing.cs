﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class HazardThing : MonoBehaviour {

		Collider col;
		// Use this for initialization
		void Start () {
			col = GetComponent<Collider> ();

			if(col.isTrigger == false){
				Debug.LogError ("Error. No Trigger.");
			}
		}

		void OnTriggerEnter(Collider other) {
			if(other.gameObject.tag == "Player"){
                other.GetComponent<Respawn>().RespawnPlayer();
			}

		}

		void OnTriggerExit(Collider Other){
			if(Other.gameObject.tag == "Player"){

			}
		}

		// Update is called once per frame
		void Update () {

		}
}
