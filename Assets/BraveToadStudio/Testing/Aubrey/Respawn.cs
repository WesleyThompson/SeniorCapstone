using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public static GameObject localPlayerInstance;
	public static GameObject spawnPoint;
	public CameraFade cameraBlack;

	// Use this for initialization
	void Start () {
		if (this.GetComponent<PhotonView>().isMine) {
			localPlayerInstance = this.gameObject;
			spawnPoint = GameObject.FindWithTag ("spawnpoint");
			//CameraFade cameraFade = Camera.main.GetComponent<CameraFade> ();
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Alpha0)) {
			//stuff to respawn the player
			RespawnPlayer();
		}
	}

	public void RespawnPlayer(){
		Debug.Log ("hello");
		Debug.Log ("Respawn Position" + localPlayerInstance.transform.position);
		localPlayerInstance.transform.position = spawnPoint.transform.position;
		CameraFade cameraFade = Camera.main.GetComponent<CameraFade> ();

		cameraFade.fadeOutIn = true;

		Debug.Log ("respawn?" + localPlayerInstance.transform.position);

	}
}
