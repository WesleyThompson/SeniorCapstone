using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class TimerScript : PunBehaviour
{
    [Range(0f, 600f)]
    public double waitTime;
    [Range(0f, 600f)]
    public double matchTime;

    bool waitTimeOver = false;
    bool matchTimeOver = false;

    void Start() {
        //Host does all rpc calls
        if (PhotonNetwork.isMasterClient) { 
            
        }
    }

    /// <summary>
    /// Alerts all clients on when the countdown started so they can set their timers accordingly
    /// </summary>
    [PunRPC]
    private void StartCountdown(double countdownStartTime, float time) {

    }

    private void SetCountdown() {

    }

    private IEnumerator Countdown(float time, bool flag) {
        while(time > 0f)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }

        flag = true;
    }
}