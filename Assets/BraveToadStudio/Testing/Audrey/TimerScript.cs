using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using System;

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
        try
        {
            transform.parent.GetComponent<GameCanvasManager>().timer = this;
        } 
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }

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

    /// <summary>
    /// Helper function that converts a float into minutes and seconds in the format 0:00
    /// </summary>
    /// <param name="time">The time in seconds</param>
    /// <returns>A formatted string representing a time</returns>
    private string ConvertToTime(float time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.Floor(time % 60);
        return minutes + ":" + seconds.ToString("00");
    }

    /// <summary>
    /// Calls the RPC StartCountdown() given that this is the master client and that this timer belongs to the local client
    /// </summary>
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

    /// <summary>
    /// Coroutine that does the initial countdown as well counting down the match time
    /// </summary>
    private IEnumerator Countdown()
    {
        if (!waitTimeOver)
        {
            while (waitTime > 0f)
            {
                yield return new WaitForEndOfFrame();
                waitTime -= Time.deltaTime;
            }

            SetWaitTimeOver();
        }
        else if (!matchTimeOver)
        {
            while (matchTime > 0f)
            {
                yield return new WaitForEndOfFrame();
                matchTime -= Time.deltaTime;
            }

            SetMatchTimeOver();
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       //Weird nonsene if I didn't implement this method
    }

    public event EventHandler OnSetWaitTimeOver;
    public event EventHandler OnSetMatchTimeOver;

    private void SetWaitTimeOver()
    {
        waitTimeOver = true;
        if(OnSetWaitTimeOver != null)
        {
            OnSetWaitTimeOver(this, null);
        }
    }

    private void SetMatchTimeOver()
    {
        matchTimeOver = true;
        if (OnSetMatchTimeOver != null)
        {
            OnSetMatchTimeOver(this, null);
        }
    }
}