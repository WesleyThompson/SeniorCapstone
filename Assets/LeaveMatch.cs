using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveMatch : MonoBehaviour {

	public Button leaveMatch;


	// Use this for initialization
	void Start () {
		leaveMatch = GetComponent<Button>();
		//leaveMatch.onClick.AddListener(TaskOnClick);

	}

	
	// Update is called once per frame
	public void TaskOnClick() {
		SceneManager.LoadScene ("Main Menu", LoadSceneMode.Single);

		//Debug.Log ("Test");
	}





}
