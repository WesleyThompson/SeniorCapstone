using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script takes user input and uses it to interact with UI elements
public class GameCanvasManager : PunBehaviour {
    public GameObject pauseMenu;
    bool isPaused = false;

    void Start() {
        pauseMenu.SetActive(isPaused);

        try
        {
            GameObject timer = PhotonNetwork.Instantiate("Timer", transform.position, transform.rotation, 0, null);
            timer.transform.SetParent(this.transform);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

	void Update () {
        if ( ( Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Controller Menu") )  && pauseMenu != null) {
            TogglePauseMenu();
        }
	}

    public void TogglePauseMenu() {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
    }

	public void LeaveMatch(){
		SceneManager.LoadScene ("Main Menu");
	}

    
}
