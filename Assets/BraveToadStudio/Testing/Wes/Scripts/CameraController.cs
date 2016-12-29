using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    GameObject player;
    public Vector3 cameraOffset;

    void LateUpdate() {
        if (player != null)
        {
            float largestScaleDir = largestScaleDirection();

            cameraOffset.y = largestScaleDir;
            cameraOffset.z = -largestScaleDir - 4;

            transform.position = player.transform.position + cameraOffset;
            transform.LookAt(player.transform);
        }
        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players) {
                if (p.GetPhotonView().isMine) {
                    player = p;
                }
            }
        }
    }

    private float largestScaleDirection() {
        float largest = player.transform.localScale.x;

        if (largest < player.transform.localScale.y)
            largest = player.transform.localScale.y;

        if (largest < player.transform.localScale.z)
            largest = player.transform.localScale.z;

        return largest;
    }
}
