using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class TimerScript : PunBehaviour
{
	public Text text;
	private float timeLeft;	
	public bool isPause = true;

	void Awake() {
		//set time to 3s
		SetTime(5f);
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
            photonView.RPC("ReportTime", PhotonTargets.AllViaServer, timeLeft);
		}

		text.text =  (timeLeft / 60 ).ToString("00")  + ":" + (timeLeft%60).ToString("00");
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
	public void SetTime(float time){
		timeLeft = time;
	}

    [PunRPC]
    private void ReportTime(float time){
        timeLeft = time;
    }
}