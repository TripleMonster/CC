using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventUtils;

enum TTCountDownType { INT_TIME, FLOAT_TIME }

public class TTCountDownTimer : MonoBehaviour 
{
	[HideInInspector] public UEvent_i intRealTimeEvent = new UEvent_i();
	public bool isBeginTimer;

	private float countDownTime;
	private int intRealTime;
	private int m_speedFactor;
	public int speedFactor
	{
		get { return m_speedFactor; }
		set { m_speedFactor = value; }
	}

	void Start () 
	{
		speedFactor = 1;
	}
	
	void Update () 
	{
		if (isBeginTimer)
		{
			if (countDownTime > 0)
				countDownTime -= Time.deltaTime * speedFactor;
			else
				countDownTime = 0;
			
			if (countDownTime == 0)
				isBeginTimer = false;
			
			CheckRealTime();
		}
	}

	void CheckRealTime()
	{
		int curTime = Mathf.CeilToInt(countDownTime);
		if (curTime < intRealTime)
		{
			intRealTime = curTime;
			intRealTimeEvent.Invoke(intRealTime);
		}
	}

	public void SetCountDownTime(float cdTime)
	{
		countDownTime = cdTime;
		intRealTime = (int)countDownTime;
	}
}
