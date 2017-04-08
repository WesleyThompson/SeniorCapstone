using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;

public class errorPopupController : MonoBehaviour
{//placed on the error button in the main menu to allow click to disable the button, acknowledging the error
	public Button errorPopup ;

	void Start ()
	{
		errorPopup.GetComponent<Button>().onClick.AddListener (closePopup);
	}

	void Update () {}

	public void closePopup()
	{
		errorPopup.gameObject.SetActive(false) ;
	}
}
