using UnityEngine;
using Photon;
using System.Collections;

// Notes:
// This script must be attached to the rigidbody of the player's ball.

public class PlayerController : Photon.PunBehaviour {

	private Rigidbody rb;
	//We have an object higher than the player to control objects
	private PhotonView parentPhotonView;
	private PhotonTransformView transformView;
	public GameObject objLayer;

	public float speed;
	[Range(0,1)]
	public float slowRate;
	public float stopThreshold;

	public float maxVelocity = 0;


	//boost variables --------------------------------------------------------------
	//Becomes true only if an controller's right trigger is pressed...
	//doesn't go false if controller is then removed 
	//if logic is in FixedUpdate
	private bool xboxController = false;
	//TODO need a better way to determine if the player is using a controller or not

	/*
	 * bostActive used for determining if a player can pushback another player
	 * boostMagnitude = strength of boost
	 * boostMagnitudeFromStandStillMagnitude = strength of boost from standstill
	 * boostChargeTime = time in seconds to charge boost
	 * chargeCounter counts the player's charge time
	 * boostReleased = true when key / trigger for boost is released
	 */
	private bool boostActive = false;
	public float boostMagnitude = (float)5;
	public float boostFromStandStillMagnitude = (float)25;
	public float boostChargeTime = 1;
	private float chargeCounter = 0;
	private bool boostReleased = false;
	/*
	 * otherPlayersTag = what other players are tagged as
	 * pushbackMagnitude = strength of pushback against other players
	 * pushbackTime = time after boost that the player can pushback another player
	 * pushbackCounter counts the player's pushbackTime
	 */
	private string otherPlayersTag = "Player";
	public float pushbackMagnitude = (float)7.5;
	public float pushbackTime = 3;
	private float pushbackCounter = 0;
	//---------------------------------------------------------------------------

	//Shape stuff
	private Vector3 sizeTarget;
	public float sizeLerpSpeed;

	//Audio stuff
	AudioSource audioNonPickups;
	AudioSource audioPickups;
	AudioSource audioGround;

	void Start () {
		rb = GetComponent<Rigidbody>();

		AudioSource[] audios = GetComponents<AudioSource> ();
		audioNonPickups = audios [0];
		audioPickups = audios [1];
		audioGround = audios [2];

		parentPhotonView = GetComponentInParent<PhotonView>();
		transformView = GetComponent<PhotonTransformView>();

		sizeTarget = transform.localScale;
		Debug.Log("Size " + sizeTarget);
	}


	void FixedUpdate () {
		if (parentPhotonView.isMine) //Make sure this is our player before controlling
		{
			// Using this allows us to get cross-platform input
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);

			// Adjust the target direction based on the direction the camera is facing
			targetDirection = Camera.main.transform.TransformDirection(targetDirection);
		//	Debug.Log (Camera.main.transform.rotation.eulerAngles);
			// Reset the Y direction of our target direction
			targetDirection.y = 0f;

			rb.AddForce(targetDirection * speed);
			//Synchronizing stuff to make other players look smoother
			transformView.SetSynchronizedValues(targetDirection * speed, rb.angularVelocity.magnitude);


		//boost implementation ---------------------------------------------------------------------
			//player is using an xboxController 
			if( Input.GetAxis("Right Trigger") < 0)
				xboxController = true;

			//IF KEY PRESSED DOWN chargeCounter increases 
			if ( (Input.GetKey (KeyCode.Alpha1) == true) || Input.GetAxis("Right Trigger") < -0.1 ){//Input.GetAxis("Right Trigger") < 0    // timeStamp <= Time.time &&
				chargeCounter += Time.deltaTime;
				if(chargeCounter >= boostChargeTime) Debug.Log ("Boost is ready ");
			}

			//boostReleased AND charge is ready 
			//DO THE BOOST
			if (boostReleased && (chargeCounter >= boostChargeTime)) { 
				//BOOST FROM STANDSTILL
				if (targetDirection.magnitude == 0) {
					Vector3 booster = Camera.main.transform.forward;
					booster.Normalize ();
					Vector3 zeroScale = new Vector3 (boostFromStandStillMagnitude,boostFromStandStillMagnitude, boostFromStandStillMagnitude);
					booster.Scale (zeroScale);
					rb.AddForce (booster, ForceMode.Impulse);
					//Debug.Log ("booster is " + booster + " plus boost is " + booster * boostMagnitude);
				} 
				else { //BOOST TOWARDS TARGETDIRECTION
					targetDirection.Scale (new Vector3 (boostMagnitude, boostMagnitude, boostMagnitude));
					rb.AddForce (targetDirection* boostMagnitude, ForceMode.VelocityChange);
				}

				//RESET COUNTERS BOOST IS ACTIVE
				chargeCounter = 0;
				pushbackCounter = 0;
				boostActive = true;
				boostReleased = false;
			} else if (boostReleased) {//key released before charge time; reset counters
				pushbackCounter = 0;
				chargeCounter = 0;
				boostReleased = false;
			}
		

			pushbackCounter += Time.deltaTime;
			//SET BOOSTACTIVE TO FALSE TO STOP PUSHING OTHER PLAYERS BACK
			if (boostActive && (pushbackCounter > pushbackTime))//hit another player <= pushbackTime after boost knocks them back
				boostActive = false;
			//end of boost implementation -------------------------------------------------------------------



			// Stop all movement if not touching controls
			// TODO this section will NOT work with controllers. Controllers sometimes never reach 0
			//     since they are analog. Need to add "Dead-Zone" aka a range of values that are close
			//     enough to 0 to call it 0.
			if (horizontal == 0f && vertical == 0f){
				rb.velocity *= slowRate;
				rb.angularVelocity *= slowRate;
			}

			//Size stuff
			if (!transform.localScale.Equals(sizeTarget)) {
				float delta = Time.deltaTime * sizeLerpSpeed;
				transform.localScale = Vector3.Lerp(transform.localScale, sizeTarget, delta);
			}


		}
	}
		
	//need to know immeadiatly if the boost charger was released
	void Update() {
		//if the player's using an xboxController check if trigger released
		if (xboxController) {
			if((Input.GetAxis ("Right Trigger") > -0.2))
				boostReleased = true;
		} 
		//if player using keyboard check if '1' was released
		else if (Input.GetKeyUp (KeyCode.Alpha1))
			boostReleased = true;
	}
		
	void OnCollisionEnter(Collision other) {
		//This grabs the object layer that belongs to our player
		if (objLayer == null)
		{
			GameObject[] objs = GameObject.FindGameObjectsWithTag("ObjectLayer");
			foreach (GameObject obj in objs)
			{
				if (obj.transform.parent.gameObject.GetPhotonView().ownerId == gameObject.GetPhotonView().ownerId)
				{
					objLayer = obj;
				}
			}
		}

		var colliderSize = other.collider.bounds.size.magnitude;
		var playerSize = GetComponent<Collider> ().bounds.size.magnitude;


		//Picking up objects adding to player
		if (other.gameObject.tag.Equals ("Pickup") && colliderSize <= playerSize) {

			//Grab the size of the pickups collider
			Vector3 size = other.collider.bounds.size;

			//Disable the collider and parent the object to the player
			other.collider.enabled = false;
			other.transform.parent = objLayer.transform;

			//Add the target size of our player
			sizeTarget = ConvertVectorToDisplacement (size);

			//Audio for collision with object the player is big enough to pickup
			//Collision must meet threshold velocity
			if (other.relativeVelocity.magnitude > 1.5) {

				//if the sound is playing don't play it a second time
				if (!GetComponent<AudioSource> ().isPlaying) { 
					audioPickups.Play ();
				}
			}
		}
		else {//collided object cannot be picked up


			//collided into another player
			if (other.gameObject.tag.Equals (otherPlayersTag)) {
				

				//TODO Steal mechanic --------------------------------------------------------------------------------
				//some condition will allow this player to steal from another
				bool stealCondition = true;

				//can't steal from another player with a minimun size 
				float minSize = 0;
				float otherPlayerSize = other.collider.bounds.size.magnitude;

				if (stealCondition && otherPlayerSize > minSize) {


					//TODO need some size to steal
					//Vector3 stealSize;



					//TODO 
					//take the size away from the other player


					//add the size to this player
					//sizeTarget = ConvertVectorToDisplacement (stealSize);

					//TODO
					//Animation of this mechanic

				}
					
				//other player needs to be pushed back if boostActive
				if (boostActive) {
					var force = -other.relativeVelocity;
					force.Normalize ();
					force.Scale (new Vector3 (pushbackMagnitude, pushbackMagnitude, pushbackMagnitude));
					other.gameObject.GetComponent<Rigidbody> ().AddForce (force);
				}
			}

			//Audio for collision with objects the player is not big enough to pickup
			//Collision must meet threshold velocity
			if (other.relativeVelocity.magnitude > 1.5) { 
				//tagged objects do not trigger ground sound
				if (other.gameObject.tag != "Untagged") {
					//if the sound is playing don't play it a second time
					if (!GetComponent<AudioSource> ().isPlaying) {
						audioNonPickups.Play ();
					}
				}
				else {//ground
					//if the sound is playing don't play it a second time
					if (!GetComponent<AudioSource> ().isPlaying) {
						audioGround.Play ();
					}
				}
			}
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

}
