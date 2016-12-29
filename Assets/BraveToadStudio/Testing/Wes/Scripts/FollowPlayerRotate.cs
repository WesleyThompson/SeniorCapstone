using UnityEngine;
using System.Collections;

public class FollowPlayerRotate : MonoBehaviour {
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position;
    }
}
