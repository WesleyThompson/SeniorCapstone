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


	//Becomes true only if an controller's right trigger is pressed...
	//doesn't go false if controller is then removed; logic is in Boost method
	//TODO need a better way to determine if the player is using a controller or not
	private bool xboxController = false;

	/* BOOST VARIABLES
	 * boostMagnitude = strength of boost when player is already moving
	 * boostMagnitudeFromStandstillMagnitude = strength of boost from standstill
	 * boostChargeTime = time in seconds to charge boost
	 * chargeCounter counts the player's charge time
	 * boostReleased = true when key / trigger for boost is released
	 */
	public float boostMagnitude = 4;
	public float boostFromStandstillMagnitude = 25;
	public float boostChargeTime = 1;
	private float chargeCounter = 0;
	private bool boostReleased = false;

	/*playerPushPlayer variables
	 * otherPlayersTag = what other players are tagged as
	 * pushbackMagnitude = strength of pushback against other players
	 * pushbackThresholdVelocity = velocity the OTHER player must be greater than to push back THIS Player
	 */
	private string otherPlayersTag = "Player";
	public float pushbackMagnitude = (float)5;//a bit stronger than boost magnitude is good maybe
	public float pushbackThresholdVelocity = 4;

	//Shape stuff
	private Vector3 sizeTarget;
	public float sizeLerpSpeed;

	//Audio stuff
	public float VelocityTriggerAudio = (float)1.5;//collision realitve velocity must exceed this to play sounds
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
		if (parentPhotonView.isMine){ //Make sure this is our player before controlling
			
			// Using this allows us to get cross-platform input
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);

			// Adjust the target direction based on the direction the camera is facing
			targetDirection = Camera.main.transform.TransformDirection(targetDirection);

			// Reset the Y direction of our target direction
			targetDirection.y = 0f;

			rb.AddForce(targetDirection * speed);
			//Synchronizing stuff to make other players look smoother
			transformView.SetSynchronizedValues(targetDirection * speed, rb.angularVelocity.magnitude);

			Boost (targetDirection);

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

	private void Boost(Vector3 targetDirection){
		//player is using an xboxController 
		if( Input.GetAxis("Right Trigger") < 0)
			xboxController = true;

		//IF KEY/TRIGGER PRESSED DOWN chargeCounter increases 
		if ( (Input.GetKey (KeyCode.Alpha1) == true) || Input.GetAxis("Right Trigger") < -0.1 ){
			chargeCounter += Time.deltaTime;
			if(chargeCounter >= boostChargeTime) Debug.Log ("Boost is ready ");
		}

		//if boostReleased AND charge is ready 
		if (boostReleased && (chargeCounter >= boostChargeTime)) {//DO THE BOOST 
			if (targetDirection.magnitude == 0) {//BOOST FROM STANDSTILL
				Vector3 booster = Camera.main.transform.forward;
				booster.Normalize ();
				Vector3 zeroScale = new Vector3 (boostFromStandstillMagnitude,boostFromStandstillMagnitude, boostFromStandstillMagnitude);
				booster.Scale (zeroScale);
				rb.AddForce (booster, ForceMode.Impulse);
			} 
			else { //BOOST TOWARDS TARGETDIRECTION
				targetDirection.Scale (new Vector3 (boostMagnitude, boostMagnitude, boostMagnitude));
				rb.AddForce (targetDirection* boostMagnitude, ForceMode.VelocityChange);
			}
			//RESET COUNTERS BOOST IS ACTIVE
			chargeCounter = 0;
			boostReleased = false;
		} else if (boostReleased) {//key released before charge time; reset counters
			chargeCounter = 0;
			boostReleased = false;
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
		GrabPlayerObjectLayer();
	
		Vector3 colliderSize = other.collider.bounds.size;//size of what player collided into
		var playerSize = GetComponent<Collider> ().bounds.size.magnitude;
		if (other.gameObject.tag.Equals (otherPlayersTag))
			CollidedIntoPlayer (other);
		else if (other.gameObject.tag.Equals ("Pickup") && colliderSize.magnitude <= playerSize)//collided into an obj that must be picked up
			CollidedIntoPickup (other, colliderSize);
		else
			CollidedIntoNonPickup (other);//collided into an obj that cannot be picked up
	}
		
	private void GrabPlayerObjectLayer(){
		if (objLayer == null){
			GameObject[] objs = GameObject.FindGameObjectsWithTag("ObjectLayer");
			foreach (GameObject obj in objs){
				if (obj.transform.parent.gameObject.GetPhotonView().ownerId == gameObject.GetPhotonView().ownerId)
					objLayer = obj;
			}
		}
	}

	private void CollidedIntoPickup(Collision other, Vector3 ColliderSize){
		//Disable the collider and parent the object to the player
		other.collider.enabled = false;
		other.transform.parent = objLayer.transform;
		//Add the target size of our player
		sizeTarget = ConvertVectorToDisplacement (ColliderSize);
		playSound(other,audioPickups);
	}

	private void CollidedIntoNonPickup(Collision other){
		if (other.gameObject.tag != "Untagged")//collision with tagged objects player can't pickup play 'audioNonPickups'
			playSound(other, audioNonPickups);
		else //collision with untagged objects player can't pickup play'audioGround'
			playSound (other, audioGround);
		
		//instantiate splat prefab and attach splatController script onto it
		foreach(ContactPoint contact in other.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white) ;
			Debug.Log("hit " + other.gameObject.name) ;
		}
	}

	private void CollidedIntoPlayer(Collision other){

		Debug.Log ("velocity on collision with palyer is " + rb.velocity.magnitude);
		//PlayerPushPlayer Implementation
		float otherPlayerVelocity = other.relativeVelocity.magnitude - rb.velocity.magnitude;
		if (otherPlayerVelocity > pushbackThresholdVelocity) {//then THIS player needs to be pushed back
			var force = other.relativeVelocity;
			force.Normalize ();
			force.Scale (new Vector3 (pushbackMagnitude, pushbackMagnitude, pushbackMagnitude));
			BumpPlayer (force);
		}
	
		//TODO steal mechanic implementation
		bool stealCondition = false;//some condition will allow this player to steal from another
		if (stealCondition)
			Steal (other);
	}

	public void BumpPlayer(Vector3 magnitude){
		rb.AddForce (magnitude, ForceMode.Impulse);
	}
		
	private void playSound(Collision other, AudioSource audio){
		if (other.relativeVelocity.magnitude > VelocityTriggerAudio) {
			if (!GetComponent<AudioSource> ().isPlaying)
				audio.Play ();
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
		
	//TODO Steal
	public float minSize = 1;//can't steal from another player with a minimun size 
	public float stealMagnitude = 1;//size that is stolen...
	private void Steal(Collision other){
		Vector3 otherPlayerSize = other.collider.bounds.size;
		if (otherPlayerSize.magnitude > minSize) {//Can steal

			//need some size to steal
			Vector3 stealSize = otherPlayerSize;
			stealSize.Normalize ();
			/*TODO
				steal size could be determined by the size of who the player steals from
				ex. steal a larger amount from a large player 
					and steal a samller amount from a small player

				or steal size could be standard
				ex. steal the same amount of size whether stealling from a large or small player
			*/
			Vector3 stealSizeScaler = new Vector3 (stealMagnitude,stealMagnitude, stealMagnitude);
			stealSize.Scale (stealSizeScaler);


			//TODO 
			//take the size away from the other player

			//add the size to this player
			sizeTarget = ConvertVectorToDisplacement (stealSize);

			//TODO
			//Animation of this mechanic
		}
	}
}
