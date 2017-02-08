/*
This script will generate visible players' display names above their character model, oriented to face each viewer
Stretch: names will only be rendered to a particular camera when visible to that camera
	names will scale based on distance from given camera

	possible snippets
	
	 private var localName:String; // Holds the local player name
     private var namePlatePos : Vector3;
     var namePlate:GUIStyle;
     
     function OnGUI()
	     {
             // Place the name plate where the gameObject (player prefab) is
             namePlatePos = Camera.main.WorldToScreenPoint(gameObject.transform.position);    
             GUI.Label(Rect((namePlatePos.x-50), (Screen.height - namePlatePos.y+10), 100, 50), localName, namePlate);    
         }
		 
		 /////in OnJoinedInstantiate
			player.AddComponent("nameofscript"); 
			or
			player.AddComponent<ScriptName>();
*/

using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;

public class OverheadDisplayer : MonoBehaviour
{

	public List<GameObject> playerList = new List<GameObject>() ;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}
}
