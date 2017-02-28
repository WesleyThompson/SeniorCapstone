using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSizeScript : MonoBehaviour {
     public Text text1;
     public GameObject player;
	// Use this for initialization
	void Start () {
          GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
           //(GetComponent<PhotonView>().isMine) - ask wes 
           foreach (GameObject g in array)
          {
               if (g.GetComponent<PhotonView>().isMine)
               {
                    player = g;
                    break;
               }

          }
          // Connect to canvas that displays Size in meters and centimeters 
          float playerSize = player.transform.localScale.x;
          int centimeters;
          int meters;
          centimeters = (int)(playerSize % 100);
          meters = (int)(playerSize / 100);
          text1.text = meters.ToString() + " m " + centimeters.ToString() + " cm"; 
     }

	
	// Update is called once per frame
	void Update () {
		
	}
}
