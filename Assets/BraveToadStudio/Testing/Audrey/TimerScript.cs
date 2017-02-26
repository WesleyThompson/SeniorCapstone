using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
	public Text text;
	private float timeLeft;	
	public bool isPause = true;

	void Awake() {
		//set time to 3s
		SetTime(3f);
		Pause();
	}

	void Start() {

	}



	void Update()
	// End graphic?
	{
		if (isPause == false && timeLeft > 0) {
			//countdown
			timeLeft -= Time.deltaTime;

		}

		//text.text =  (timeLeft / 60 ).ToString("00")  + ":" + (timeLeft%60).ToString("00");
			//timeLeft.ToString();
	}

	//pauses timer when called 
	// stop counting
	// Bool function?
	public void Pause()
	{
		isPause = true;
					
	}

	//set variable and run update
	// start counting
	//bool function? 
	public void Play(){

		isPause = false;
	}

	//set time 
	//have default states
	public void SetTime(float time)
	{
		timeLeft = time;
	}
}