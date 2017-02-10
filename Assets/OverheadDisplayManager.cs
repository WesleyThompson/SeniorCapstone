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
	public Camera main ;
	public GameObject rockAsPlayer ;
	Vector3 playerPosOnScreen ;

	public List<GameObject> playerList = new List<GameObject>() ;

	void Start ()
	{
	}
	
	void Update()
	{
		playerPosOnScreen = Camera.main.WorldToScreenPoint(rockAsPlayer.transform.position) ;
	}
	void OnGUI()
	{
		GUI.Label(new Rect(playerPosOnScreen.x, playerPosOnScreen.y, 100, 50), "testinggg" ) ;
	}
}
