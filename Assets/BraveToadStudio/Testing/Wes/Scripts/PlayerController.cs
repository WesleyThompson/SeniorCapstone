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

    public float speed;
	[Range(0,1)]
	public float slowRate;
    public float stopThreshold;

	void Start () {
        rb = GetComponent<Rigidbody>();
        parentPhotonView = GetComponentInParent<PhotonView>();
        transformView = GetComponent<PhotonTransformView>();
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

            // Stop all movement if not touching controls
            // TODO this section will NOT work with controllers. Controllers sometimes never reach 0
            //     since they are analog. Need to add "Dead-Zone" aka a range of values that are close
            //     enough to 0 to call it 0.
            if (horizontal == 0f && vertical == 0f)
            {
                rb.velocity *= slowRate;
                rb.angularVelocity *= slowRate;
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        //If this isn't our player then don't do anything
        if (!parentPhotonView.isMine)
            return;

        if (other.gameObject.tag.Equals("Pickup")) {
            Debug.Log("Bounds: " + other.collider.bounds.size.ToString());
            Vector3 size = other.collider.bounds.size;
            // Adjust size to match rotation
            Debug.Log("Size: " + size.ToString());
            transform.localScale += size;
        }
    }
}
