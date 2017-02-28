using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PunBehaviour {

    private TimerScript timer;
    private GameCanvasManager gcManager;

    void Awake ()
    {
        gcManager = GameObject.FindObjectOfType<GameCanvasManager>();
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
        //TODO replace some of this mess with some sweet event driven stuff
        if (timer == null)
        {
            if(gcManager.timer != null)
            {
                timer = gcManager.timer;
            }
        }
        else
        {
            if(timer.waitTimeOver)
            {
                Debug.Log("Match Started");
            }
            if(timer.matchTimeOver)
            {
                Debug.Log("Match Concluded");
                LoadWinScene();
            }
        }
	}

    private void LoadWinScene() {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("Main Menu");
        }
    }
}
