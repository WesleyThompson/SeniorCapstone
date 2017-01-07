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

    //Shape stuff
    private Vector3 sizeTarget;
    public float sizeLerpSpeed;

	void Start () {
        rb = GetComponent<Rigidbody>();
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

        if (other.gameObject.tag.Equals("Pickup") && other.collider.bounds.size.magnitude <= GetComponent<Collider>().bounds.size.magnitude) {
            //Grab the size of the pickups collider
            Vector3 size = other.collider.bounds.size;

            //Disable the collider and parent the object to the player
            other.collider.enabled = false;
            other.transform.parent = objLayer.transform;

            //Add the target size of our player
            sizeTarget = ConvertVectorToDisplacement(size);
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
