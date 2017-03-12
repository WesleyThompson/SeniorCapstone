using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PunBehaviour {

    private TimerScript timer;
    private GameCanvasManager gcManager;
    private GameObject[] players;

    private bool hasTimer = false;

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
                TimerSetup();
            }
        }
	}

    private void LoadWinScene() {
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.LoadLevel("LobbyIslandForreal");
        }
    }

    private void TimerSetup()
    {
        //This makes sure the setup is only called once
        if(hasTimer)
        {
            return;
        }

        //Set Listeners
        timer.OnSetWaitTimeOver += HandleWaitTimeOver;
        timer.OnSetMatchTimeOver += HandleMatchTimeOver;
        hasTimer = true;
    }

    private void HandleWaitTimeOver(object sender, EventArgs e)
    {
        Debug.Log("Match Started");
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void HandleMatchTimeOver(object sender, EventArgs e)
    {
        Debug.Log("Match Concluded");
        DecideWinner();
        LoadMainMenu();
    }

    private void DecideWinner()
    {
        float maxScale = 0f;
        GameObject currentMaxPlayer = null;
        //TODO check for ties
        foreach(GameObject player in players)
        {
            if(player.transform.localScale.x > maxScale)
            {
                currentMaxPlayer = player;
                maxScale = player.transform.localScale.x;
            }
        }

        Debug.Log("The winner is " + currentMaxPlayer.name + " with a diameter of " + maxScale + " meters");
    }

    private void LoadMainMenu()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }
        PhotonNetwork.LoadLevel("Main Menu");
    }
}
