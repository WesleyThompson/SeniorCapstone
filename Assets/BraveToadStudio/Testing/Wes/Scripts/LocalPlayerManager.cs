using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerManager : MonoBehaviour {

    public static GameObject localPlayerInstance;
	public static GameObject spawnPoint;

    void Start() {
        if (this.GetComponent<PhotonView>().isMine) {
            localPlayerInstance = this.gameObject;
			spawnPoint = GameObject.FindWithTag ("spawnpoint");
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

	void Update() {
		if(Input.GetKeyUp(KeyCode.Alpha0)) {
			//stuff to respawn the player
			Respawn();
		}
	}

	public void Respawn(){
		localPlayerInstance.transform.position = spawnPoint.transform.position;
		Debug.Log ("respawn?");
		
	}

}
