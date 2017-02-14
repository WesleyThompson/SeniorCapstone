using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerManager : MonoBehaviour {

    public static GameObject localPlayerInstance;

    void Start() {
        if (this.GetComponent<PhotonView>().isMine) {
            localPlayerInstance = this.gameObject;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
}
