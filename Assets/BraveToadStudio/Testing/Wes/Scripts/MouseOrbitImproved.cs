using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f);
    public float xSpeed = 30.0f;
    public float ySpeed = 30.0f;
    public float xControllerSpeed = 120.0f;
    public float yControllerSpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rb;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rb = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            x += Input.GetAxis("Joystick X") * xControllerSpeed * 0.02f;
            y -= Input.GetAxis("Joystick Y") * yControllerSpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = target.localScale.magnitude;

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position + offset;

            transform.rotation = rotation;
            transform.position = position;
        }
        else {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
            {
                if (p.GetPhotonView().isMine)
                {
                    target = p.transform;
                }
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}