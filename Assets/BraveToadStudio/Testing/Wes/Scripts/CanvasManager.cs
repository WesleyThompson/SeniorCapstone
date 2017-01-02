using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script takes user input and uses it to interact with UI elements
public class CanvasManager : MonoBehaviour {
    public GameObject pauseMenu;
    bool isPaused = false;

    void Start() {
        pauseMenu.SetActive(isPaused);
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
}
