using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class TimerScript : PunBehaviour
{
    public Text timerText;

    [Range(0f, 600f)]
    public float waitTime;
    [Range(0f, 600f)]
    public float matchTime;

    public bool waitTimeOver = false;
    public bool matchTimeOver = false;
    private bool ranWaitTime = false;

    void Start()
    {
        Debug.Log("StartMethod");
        CallStartCountdown();
    }

    void Update() {
        if (!waitTimeOver)
        {
            timerText.text = ConvertToTime(waitTime);
        }
        else if(!matchTimeOver)
        {
            if(!ranWaitTime)
            {
                CallStartCountdown();

                ranWaitTime = true;
            }

            timerText.text = ConvertToTime(matchTime);
        }
    }

    private string ConvertToTime(double time)
    {

        return Mathf.Floor((float) time / 60).ToString() + ":" + (time % 60).ToString("00");
    }

    private void CallStartCountdown()
    {
        Debug.Log("RunningRPC");
        //Host does all rpc calls
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("StartCountdown", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
    }

    /// <summary>
    /// Alerts all clients on when the countdown started so they can set their timers accordingly
    /// </summary>
    [PunRPC]
    private void StartCountdown(double countdownStartTime)
    {
        
        if (!waitTimeOver)
        {
            waitTime -= (float) (PhotonNetwork.time - countdownStartTime); 
        }
        else if(!matchTimeOver)        
        {
            matchTime -= (float)(PhotonNetwork.time - countdownStartTime);
        }

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        if (!waitTimeOver)
        {
            while (waitTime > 0f)
            {
                yield return new WaitForEndOfFrame();
                waitTime -= Time.deltaTime;
            }

            waitTimeOver = true;
        }
        else if (!matchTimeOver)
        {
            while (matchTime > 0f)
            {
                yield return new WaitForEndOfFrame();
                matchTime -= Time.deltaTime;
            }

            matchTimeOver = true;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       //Weird nonsene if I didn't implement this method
    }
}