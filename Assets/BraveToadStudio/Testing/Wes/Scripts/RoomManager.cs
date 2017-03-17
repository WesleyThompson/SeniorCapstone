using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.SceneManagement;

public class RoomManager : PunBehaviour {

    public string mainSceneName;

	// Use this for initialization
	void Start () {
        PhotonNetwork.automaticallySyncScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnJoinedRoom() {
        Debug.Log("wassup fam");
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("wassup fam vol 2");
            if (PhotonNetwork.playerList.Length == PhotonNetwork.room.MaxPlayers) {
                PhotonNetwork.LoadLevel(mainSceneName);
            }
        }
    }
}
