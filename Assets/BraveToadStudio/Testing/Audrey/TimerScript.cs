using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
	float timeLeft = 300.0f;

	public Text text;

	void Update()
	{
		timeLeft -= Time.deltaTime;
		Debug.Log (timeLeft);
		text.text = "hello";
		text.text = "Time Left:" + Mathf.Round (timeLeft);
		if (timeLeft < 0) {
			//TO DO
		}
	}

	//pause 
	void Pause()
	{
			
	}

	//stop
	void Stop()
	{

	}

	//set time 
	//have default states
	void SetTime()
	{

	}
}