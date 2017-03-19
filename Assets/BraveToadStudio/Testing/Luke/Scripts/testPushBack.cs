using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPushBack : MonoBehaviour {

	private Rigidbody rb;
	private string otherPlayersTag = "Player";
	public float pushbackMagnitude = 10;
	public float pushbackThresholdSpeed = 2;

	private Vector3 sizeTarget;

	void Start () {
		rb = GetComponent<Rigidbody>();
		sizeTarget = transform.localScale;
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag.Equals (otherPlayersTag)) {



			//Pushback; PlayerPushPlayer --------------------------------------------------------------

			/*
			float otherPlayerSpeed = other.relativeVelocity.magnitude - rb.velocity.magnitude;
			if (otherPlayerSpeed > pushbackThresholdSpeed) {//then THIS player needs to be pushed back
				var force = other.relativeVelocity;
				force.Normalize ();
				force.Scale (new Vector3 (pushbackMagnitude, pushbackMagnitude, pushbackMagnitude));
				BumpPlayer (force);
			}
			//-----------------------------------------------------------------------------------------
			*/








			//steal
			Debug.Log("calling steal in the test script");
			Steal (other);

















		}
	}


	public void BumpPlayer(Vector3 magnitude){
		rb.AddForce (magnitude, ForceMode.Impulse);
	}
		




	public float percentToSteal = (float)0.5;
	private void Steal(Collision other){
		Vector3 otherPlayerSize = other.collider.bounds.size;
		float playerSize = GetComponent<Collider> ().bounds.size.magnitude;

		if (playerSize < otherPlayerSize.magnitude ) {//THIS player can steal from OTHER player
			//Debug.Log("In if in test script");


			//need some size to steal
			Vector3 stealSize = otherPlayerSize;
			Vector3 originalOtherSize = otherPlayerSize;
			stealSize.Normalize ();

			//take from other
			float percentToDecrease = originalOtherSize.magnitude * (1 - percentToSteal);
			Vector3 stealSizeForOther = otherPlayerSize;
			stealSizeForOther.Normalize ();
			stealSizeForOther.Scale (new Vector3 (percentToDecrease,percentToDecrease, percentToDecrease));

			//take the size away from the other player
			other.collider.GetComponent<PlayerController> ().changeSize(stealSizeForOther);
			Debug.Log ("in test size from other is originally " + originalOtherSize.magnitude + "now it is " + stealSizeForOther.magnitude);

			float LargerB4StealSizeMinusAfterStealSize = originalOtherSize.magnitude - stealSizeForOther.magnitude;
			float growByThis = playerSize + LargerB4StealSizeMinusAfterStealSize;
			//Debug.Log ("playerSize is " + playerSize + " growByThis is " + growByThis);

			//grow self
			stealSize.Scale (new Vector3 (growByThis,growByThis, growByThis));
			changeSize (stealSize);
			//Debug.Log ("size for self is " + stealSize.magnitude);

			//TODO
			//Animation of this mechanic
		}
	}
		
	private Vector3 ConvertVectorToDisplacement(Vector3 size) {
		float total = size.x + size.y + size.z;
		total = total / 3;

		float currentVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(transform.localScale.x / 2, 3f);
		currentVolume += total;
		Debug.Log("CurrVol: " + currentVolume);

		float radius = Mathf.Pow( (3f/4f) * (currentVolume / Mathf.PI), 1f/3f);
		Debug.Log("New Radius " + radius);
		float diameter = radius * 2.25f;

		Vector3 newBounds = new Vector3(diameter, diameter, diameter);

		//Calculate spherical change

		return newBounds;
	}

	public void changeSize(Vector3 newSizeTarget){
		Debug.Log ("called change size()");
		sizeTarget = newSizeTarget;
		//sizeTarget = newSizeTarget;
	}




























}





