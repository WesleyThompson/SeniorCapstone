using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraFade : MonoBehaviour {

	public bool fadeIn = false;
	public bool fadeOut = false;
	public bool fadeOutIn = false;
	public float fadeInTime = 3f;
	public bool cameraBlack = false;
	public bool cameraClear = false;
	public float fadeOutTime = 3f;
	public float fadeOutInBlackHoldTime = 3;
	bool alreadyFadedOut = false;
	public float vignetting;
	float t = 0;

	void FixedUpdate(){
		if (cameraBlack)
			CameraBlack ();
		if (cameraClear)
			CameraClear ();
		var vignette = gameObject.GetComponent<VignetteAndChromaticAberration> ();
		vignette.intensity = vignetting;

		t+= Time.deltaTime;
		if (fadeOutIn)
			FadeOutIn ();
		else if (fadeIn)
			FadeIn ();
		else if (fadeOut)
			FadeOut ();
		else
			t = 0;
	}

	bool Fader (float intitialIntensity,float changedIntensity,float time,ref bool condition){
		vignetting = Mathf.Lerp (intitialIntensity, changedIntensity, time);
		if (vignetting == changedIntensity){
			condition = false;
			t = 0;
			return true;
		}
		return false;
	}

	bool FadeIn(){
		fadeIn = true;
		return Fader (1, 0, t / fadeInTime,ref fadeIn);
	}

	bool FadeOut(){
		fadeOut = true;
		return Fader(0,1,t / fadeOutTime,ref fadeOut);
	}
		
	private bool alreadyWaited = false;
	void FadeOutIn(){
		fadeOutIn = true;
        if (!alreadyFadedOut)
        {
            //didn't fade out? then FadeOut
            alreadyFadedOut = FadeOut();
            Debug.Log("time is1st " + t);
        }
        else if (t < fadeOutInBlackHoldTime && alreadyWaited == false)
        {
            //do nothing hold black screen
        }
        else if (!alreadyWaited)
        {
            alreadyWaited = true;
            t = 0;
        }
        else if (!FadeIn())
        {
            //fadeIn not complete then continue to FadeIn
            FadeIn();
        }
        else
        {
            //finished reset variables
            fadeOutIn = false;
            alreadyFadedOut = false;
            alreadyWaited = false;
        }
	}

	void CameraBlack(){
		vignetting = 1;
		cameraBlack = false;
	}

	void CameraClear(){
		vignetting = 0;
		cameraClear = false;
	}
}