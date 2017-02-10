/*
This script will display other users' names above their character model to the player vie GUIskin (automatically orients text?)
Stretch: names will only be rendered when entering field of view
	names will scale based on distance from given camera

REQUIREMENTS
UI text placed over every character that is not local player
ui text is set to player's name ... PhotonNetwork.playerName
ui text moves with player and is not seen when local player could not see it (camera)
*/	
	
using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;

public class OverheadDisplayManager : MonoBehaviour
{

	public List<GameObject> playerList = new List<GameObject>() ;

	void Start ()
	{
		//TODO fetch photon player list and init list with names (these dont change)
	}
	
	void onGUI()
	{
	}
	
	void Update ()
	{
		
	}
}
