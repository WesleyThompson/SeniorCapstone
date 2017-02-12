/*
This script will display other users' names above their character model to the player via GUIskin (automatically orients text)

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
	public Camera main ;
	public GameObject rockAsPlayer ;
	Vector3 playerPosOnScreen ;
	
	public List<GameObject> playerList = new List<GameObject>() ;
	
	void Start ()
	{
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{//collect all players in game at start
			playerList.Add(player) ;
		}
	}
	
	void Update()
	{
	}
	
	void OnGUI()
	{
		foreach(GameObject player in playerList)
		{//updates each player object's label to adhere to their positions
			playerPosOnScreen = Camera.main.WorldToScreenPoint(player.transform.position) ;
			GUI.Label(new Rect(playerPosOnScreen.x, Screen.height - playerPosOnScreen.y-100, 100, 50), player.name ) ;
		}
		playerPosOnScreen = Camera.main.WorldToScreenPoint(rockAsPlayer.transform.position) ;
		GUI.Label(new Rect(playerPosOnScreen.x, Screen.height - playerPosOnScreen.y-100, 100, 50), rockAsPlayer.name ) ;
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		//TODO get inPlayer's gameobject position and add them to the playerList
	} 	
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		//TODO remove player from the playerList
	} 
}
