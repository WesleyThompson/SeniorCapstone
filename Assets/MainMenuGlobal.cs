/*
Change Lists to type GameObject and cast Buttons like
playButton.GetComponent<GameObject>()

This script works by adding all menu items (buttons) into a List of Lists<Button>
When you click on a button that has another menu associated with it, then all current menu items will be hidden, added to the hiddenMenus List (as a stack), the buttons associated with the clicked button will be unhidden, and the currentMenu List will be overwritten with the new List of buttons, and the BACK button will be unhidden.

The BACK button will: find the List of Lists<Button> that is at the top of the stack, hide all buttons in the currentMenu List, unhide all menu items in the List that was found at the top of the stack, and then remove that List from the stack. If hiddenMenus List is ever empty, the back button will be hidden.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuGlobal : MonoBehaviour
{
	
	public List<List<Button>> hiddenMenus = new List<List<Button>>() ; //stack that holds list of all hidden menus that are traversable with the BACK button
	public List<Button> hiddenMenuItems = new List<Button>() ; //temp holder to hold items wished to be hidden / unhidden
	public List<Button> currentMenuItems = new List<Button>() ; //always holed latest set of menu items
	
	public Button playButton ;
	public Button settingsButton ;
	public Button creditsButton ;
	public Button exitButton ;
	public Button backButton ;

	public Text creditText ;
	public GameObject settingsPanel ;
	
	void Start ()
	{//when init scene, add onclick events for all main menu buttons: play, settings, credits, exit
		playButton.GetComponent<Button>().onClick.AddListener(openPlay) ;
		settingsButton.GetComponent<Button>().onClick.AddListener(openSettings) ;
		creditsButton.GetComponent<Button>().onClick.AddListener(openCredits) ;
		exitButton.GetComponent<Button>().onClick.AddListener(closeGame) ;
		backButton.GetComponent<Button>().onClick.AddListener(goBack) ;
			backButton.gameObject.SetActive(false) ; //used for hiding/deactivating button, set to false by default
			
		creditText.gameObject.SetActive(false) ;
		settingsPanel.gameObject.SetActive(false) ;

		this.currentMenuItems = this.getAllMenuItems() ;
	}
	
	void Update () {}

    public void PlayGame() {
        SceneManager.LoadScene("LobbyIslandForreal");
    }

	public List<Button> getAllMenuItems()
	{//returns list of all currently unhidden menu items
		if(this.currentMenuItems.Count == 0)
		{//if menu has not been added to current menu items list, add them
			currentMenuItems.Add(playButton) ;
			currentMenuItems.Add(settingsButton) ;
			currentMenuItems.Add(creditsButton) ;
			currentMenuItems.Add(exitButton) ;
		}
		return currentMenuItems ;
	}
	
	public void hideMenuElements()
	{//hides all current menu items, adds them to hiddenMenu List as stack
		hiddenMenuItems.Clear() ;
		foreach(Button menuItem in currentMenuItems)
		{
			hiddenMenuItems.Add(menuItem) ;
			menuItem.gameObject.SetActive(false) ;
		}
		hiddenMenus.Add(hiddenMenuItems) ;
	}
	
	public void showHiddenMenuElements()
	{//shows the last menu item set that was hidden and removes it from the stack, for the BACK button
		currentMenuItems.Clear() ;
		foreach(Button hiddenItem in (hiddenMenus[hiddenMenus.Count-1]))
		{
			currentMenuItems.Add(hiddenItem) ;
			hiddenItem.gameObject.SetActive(true) ; //unhides button
		}
		hiddenMenus[hiddenMenus.Count-1].Clear() ; //empties last element's elements on the hiddenMenu list
		hiddenMenus.Remove(hiddenMenus[hiddenMenus.Count-1]) ; //actually removes previously emptied element
	}
	
	public void openPlay()
	{//TODO - onClick place player into waiting room
	}
	
	public void openSettings()
	{
		this.hideMenuElements() ;
		settingsPanel.gameObject.SetActive(true) ;
		backButton.gameObject.SetActive(true) ;
	}
	
	public void openCredits()
	{
		this.hideMenuElements() ;
		creditText.gameObject.SetActive(true) ;
		backButton.gameObject.SetActive(true) ;
	}
	
	public void goBack()
	{
		//TODO if any non-buttons from the settings menu are displayed, hide them
		
		if(creditText.gameObject.activeSelf)
			creditText.gameObject.SetActive(false) ;
		if(settingsPanel.gameObject.activeSelf)
			settingsPanel.gameObject.SetActive(false) ;

		
		showHiddenMenuElements() ;
		if(hiddenMenus.Count < 2)
			backButton.gameObject.SetActive(false) ; //hide back button if back at main menu
	}
	
	public void closeGame()
	{
		Application.Quit() ;
	}
}