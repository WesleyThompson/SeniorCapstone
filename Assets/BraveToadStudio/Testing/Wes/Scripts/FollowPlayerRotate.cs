using UnityEngine;
using System.Collections;

public class FollowPlayerRotate : MonoBehaviour {
    GameObject player;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.transform.parent.gameObject.GetPhotonView().ownerId == gameObject.GetPhotonView().ownerId)
            {
                player = p;
            }
        }
    }

    void FixedUpdate() {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position;
    }
}
