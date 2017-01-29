using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class MainMenuGlobal : MonoBehaviour {
	
	public List<List<Button>> hiddenMenus = new List<List<Button>>() ;
	public List<Button> hiddenMenuItems = new List<Button>() ;
	public List<Button> currentMenuItems = new List<Button>() ;
	
	public Button playButton ;
	public Button settingsButton ;
	public Button creditsButton ;
	public Button exitButton ;
	public Button backButton ;
	
	// Use this for initialization
	void Start ()
	{
		playButton.GetComponent<Button>().onClick.AddListener(openPlay) ;
		settingsButton.GetComponent<Button>().onClick.AddListener(openSettings) ;
		creditsButton.GetComponent<Button>().onClick.AddListener(openCredits) ;
		exitButton.GetComponent<Button>().onClick.AddListener(closeGame) ;
		backButton.GetComponent<Button>().onClick.AddListener(goBack) ;
			backButton.GetComponent<GameObject>().SetActive(false) ;
			//ERROR no GameObject on backButton...

		this.currentMenuItems = this.getAllMenuItems() ;
	}
	
	// Update is called once per frame
	void Update () {}
	
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
	{//hides all current menu items
		hiddenMenuItems.Clear() ;
		foreach(Button menuItem in currentMenuItems)
		{
			hiddenMenuItems.Add(menuItem) ;
			menuItem.GetComponent<GameObject>().SetActive(false) ;
		}
		hiddenMenus.Add(hiddenMenuItems) ;
	}
	
	public void showHiddenMenuElements()
	{//shows the last menu item set that was hidden, for the back button
		
		foreach(Button hiddenItem in (hiddenMenus[hiddenMenus.Count-1]))
		{
			currentMenuItems.Add(hiddenItem) ;
			hiddenItem.GetComponent<GameObject>().SetActive(true) ; //unhides button
		}
		hiddenMenus[hiddenMenus.Count-1].Clear() ;
		hiddenMenus.Remove(hiddenMenus[hiddenMenus.Count-1]) ;
	}
	
	public void openPlay()
	{//TODO - onClick place player into waiting room
	}
	
	public void openSettings()
	{
		this.hideMenuElements() ;
		//TODO - put sample settings on the menu - slider, toggle, radio selectors, value input
			//make back button visible
	}
	
	public void openCredits()
	{
		this.hideMenuElements() ;
		//TODO - display simple credits page as text
			//make back button visible
	}
	
	public void goBack()
	{
		//TODO - if credits are displayed, hide them
		//if any non-buttons from the settings menu are displayed, hide them
		showHiddenMenuElements() ;
	}
	
	public void closeGame()
	{
		Application.Quit() ;
	}
}