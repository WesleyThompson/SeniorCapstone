/*
This script, will scale that object up in size until a certain amount of time passes, then delete that object
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatController : MonoBehaviour
{
	private float xScaleFactor, yScaleFactor, zScaleFactor, duration, startTime ;
	public GameObject splat ;
	void Start ()
	{
		startTime = Time.time ;
		Vector3 originScaleFactor = this.transform.localScale ;
		duration = 1 ; //time in seconds
		xScaleFactor = Time.deltaTime ;
		yScaleFactor = 0 ; //0 indicates "up" axis
		zScaleFactor = Time.deltaTime ;
	}
	
	void Update ()
	{
		this.transform.localScale += new Vector3(xScaleFactor, yScaleFactor, zScaleFactor);
		if(Time.time >= startTime + duration)
		{
			Destroy(this.gameObject) ;
		}
	}
}
