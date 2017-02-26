using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class TimerScript : PunBehaviour
{
    public Text timerText;

    [Range(0f, 600f)]
    public double waitTime;
    [Range(0f, 600f)]
    public double matchTime;

    public bool waitTimeOver = false;
    public bool matchTimeOver = false;

    void Start()
    {
        //Host does all rpc calls
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("StartCountdown", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
    }

    void Update() {
        if (!waitTimeOver)
        {
            timerText.text = ConvertToTime(waitTime);
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                photonView.RPC("StartCountdown", PhotonTargets.AllBuffered, PhotonNetwork.time);
            }
            timerText.text = ConvertToTime(matchTime);
        }
    }

    private string ConvertToTime(double time)
    {
        return (time / 60).ToString() + ":" + (time % 60).ToString("00");
    }

    /// <summary>
    /// Alerts all clients on when the countdown started so they can set their timers accordingly
    /// </summary>
    [PunRPC]
    private void StartCountdown(double countdownStartTime)
    {
        if (!waitTimeOver)
        {
            waitTime -= PhotonNetwork.time - countdownStartTime; 
        }
        else if(!matchTimeOver)        
        {
            matchTime -= PhotonNetwork.time - countdownStartTime;
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
       
    }
}