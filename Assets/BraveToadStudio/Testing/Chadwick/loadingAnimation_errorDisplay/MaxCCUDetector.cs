using Photon ;
using System ;
using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;

public class NewBehaviourScript : PunBehaviour
{
	public GameObject errorPopup ;

	override public void OnPhotonMaxCccuReached() 
	{//when max ccu is reached, set local machine variable "maxccu" in playerpref file to 1, meaning true
		PlayerPrefs.SetInt("maxccu",1) ;
		//then load man menu, which will read this file variable and display appropriate error message
		SceneManager.LoadScene("Main Menu") ;
	}
}
