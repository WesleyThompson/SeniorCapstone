﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasManager : MonoBehaviour {

    private float penaltyTime;
    private MainMenuGlobal mmg;

    private bool playButtonActive = true;
	void Start () {
        mmg = GetComponentInChildren<MainMenuGlobal>();
        
        Debug.Log("Penalty Timer:" + penaltyTime);
	}
	
	void Update () {
        penaltyTime = PlayerPrefs.GetFloat("penaltyTimer");

        if (penaltyTime > 0)
        {
            //TODO add visual UI element for penalty time
            penaltyTime -= Time.deltaTime;
            PlayerPrefs.SetFloat("penaltyTimer", penaltyTime);

            if (playButtonActive)
            {
                playButtonActive = false;
                deactivatePlayButton();
            }
        }
        else
        {
            if (!playButtonActive)
            {
                playButtonActive = true;
                activatePlayButton();
            }
        }
	}

    private void deactivatePlayButton()
    {
        //mmg.playButton.enabled = false;
    }

    private void activatePlayButton()
    {
        //mmg.playButton.enabled = true;
    }
}
