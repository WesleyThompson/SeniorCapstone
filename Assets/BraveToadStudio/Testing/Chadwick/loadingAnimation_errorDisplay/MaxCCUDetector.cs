using Photon ;
using System ;
using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;

public class NewBehaviourScript : PunBehaviour
{
	public GameObject errorPopup ;
	void Start()
	{

	}

	void Update()
	{

	}

	void OnPhotonMaxCccuReached() 
	{
		PlayerPrefs.SetInt("maxccu",1) ;
		SceneManager.LoadScene("Main Menu") ;
	}
}
