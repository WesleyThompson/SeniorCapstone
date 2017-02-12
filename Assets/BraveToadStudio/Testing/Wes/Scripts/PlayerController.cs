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

	public bool boostActive = false;
	public float boostMagnitude = (float)30;
	public float boostPushbackMagnitude = (float)20;
	public float boostCooldown = 3;
	private float timeStamp;


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
			// Reset the Y direction of our target direction
			targetDirection.y = 0f;

			rb.AddForce(targetDirection * speed);
			//Synchronizing stuff to make other players look smoother
			transformView.SetSynchronizedValues(targetDirection * speed, rb.angularVelocity.magnitude);


			//boost while '1' key is pressed down or Right Trigger
			if (timeStamp <= Time.time && (Input.GetKey (KeyCode.Alpha1) == true || Input.GetAxis
				("Right Trigger") < 0)){
				boostMagnitude -= rb.velocity.magnitude;
				if(boostMagnitude < 0)
						boostMagnitude = 0;
				rb.AddForce (targetDirection * boostMagnitude, ForceMode.VelocityChange);
				timeStamp = Time.time+boostCooldown;
				boostActive = true;
			} else {
				if(Time.deltaTime - (timeStamp-boostCooldown) > 3)//hit another player <=3secs after boost knocks them back
					boostActive = false;
			}
			//ISSUE collisions stops boost this may be unintended
			//ISSUE boost is fast for small players yet is hardly noticible for larger players


			// Stop all movement if not touching controls
			// TODO this section will NOT work with controllers. Controllers sometimes never reach 0
			//     since they are analog. Need to add "Dead-Zone" aka a range of values that are close
			//     enough to 0 to call it 0.
			if (horizontal == 0f && vertical == 0f)
			{
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
			if (other.gameObject.tag.Equals ("Player")) {
				//other player needs to be pushed back if boostActive
				if (boostActive) {
					var force = -other.relativeVelocity;
					other.gameObject.GetComponent<Rigidbody> ().AddForce (force * 
						boostPushbackMagnitude);
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
