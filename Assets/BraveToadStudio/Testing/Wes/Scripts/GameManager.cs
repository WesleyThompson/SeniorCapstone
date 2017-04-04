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

        //Set our penalty timer in case the player leaves
        PlayerPrefs.SetFloat("penaltyTimer", 60f);
    }

    private void HandleMatchTimeOver(object sender, EventArgs e)
    {
        Debug.Log("Match Concluded");
        DecideWinner();
        LoadMainMenu();

        //Remove the penalty timer since the match concluded successfully
        PlayerPrefs.SetFloat("penaltyTimer", 0f);
    }

    private void DecideWinner()
    {
        float maxScale = 0f;
        GameObject currentMaxPlayer = null;
        //TODO check for ties
        List<GameObject> playerList = new List<GameObject>(players);
        playerList.Sort((x, y) => x.transform.localScale.x.CompareTo(y.transform.localScale.x));
        foreach(GameObject go in playerList)
        {
            Debug.Log(go.transform.localScale.x);
        }

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
            PhotonNetwork.DestroyImmediate();
        }
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnectedFromPhoton()
    {
        PhotonNetwork.LoadLevel("Main Menu");
    }
}
