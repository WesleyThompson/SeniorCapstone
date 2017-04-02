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
		SceneManager.LoadScene("Main Menu") ;
		PlayerPrefs.SetInt("maxccu",1) ;
	}
}
