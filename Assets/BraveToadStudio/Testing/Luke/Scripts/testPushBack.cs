using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPushBack : MonoBehaviour {

	private Rigidbody rb;
	private string otherPlayersTag = "Player";
	public float pushbackMagnitude = 10;
	public float pushbackThresholdSpeed = 2;


	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals (otherPlayersTag)) {



			//Pushback; PlayerPushPlayer --------------------------------------------------------------

			float otherPlayerSpeed = other.relativeVelocity.magnitude - rb.velocity.magnitude;

			if (otherPlayerSpeed > pushbackThresholdSpeed) {//then THIS player needs to be pushed back
				var force = other.relativeVelocity;
				force.Normalize ();
				force.Scale (new Vector3 (pushbackMagnitude, pushbackMagnitude, pushbackMagnitude));
				BumpPlayer (force);
			}
			//-----------------------------------------------------------------------------------------
		}
	}


	public void BumpPlayer(Vector3 magnitude){
		rb.AddForce (magnitude, ForceMode.Impulse);
	}
}
