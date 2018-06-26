using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEventUtils;

public enum TTCountDownType { INT_TIME, FLOAT_TIME }

public class TTCountDownTimer : MonoBehaviour 
{
	[HideInInspector] public UEvent_i intRealTimeEvent = new UEvent_i();
	[HideInInspector] public bool isBeginTimer;
	public TTCountDownType currentCountDownType;

	private float countDownTime;
	private int intRealTime;      // 单位 秒
	private int m_speedFactor;
	public int speedFactor
	{
		get { return m_speedFactor; }
		set { m_speedFactor = value; }
	}
	private Action<string> realTimeEvent;

	void Start () 
	{
		
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
			
			if (currentCountDownType == TTCountDownType.INT_TIME)
				CheckRealTime();
			
			if (currentCountDownType == TTCountDownType.FLOAT_TIME)
				CheckFloatTime();
		}
	}

	public void InitCoutDownTimer(TTCountDownType cdType,float cdTime, Action<string> cdEvent, int cdSpeed=1)
	{
		currentCountDownType = cdType;
		countDownTime = cdTime;
		speedFactor = cdSpeed;
		realTimeEvent = cdEvent;

		if (currentCountDownType == TTCountDownType.INT_TIME)
		{
			intRealTime = (int)countDownTime;
			CheckRealTime();
		}

		if (currentCountDownType == TTCountDownType.FLOAT_TIME)
			CheckFloatTime();
	}

	void CheckRealTime()
	{
		int curTime = Mathf.CeilToInt(countDownTime);
		if (curTime <= intRealTime)
		{
			intRealTime = curTime;
			if (realTimeEvent != null)
				realTimeEvent(intRealTime.ToString());
		}
	}

	void CheckFloatTime()
	{
		int minuteTime = Mathf.CeilToInt(countDownTime / 60);
		int secondTime = Mathf.CeilToInt(countDownTime % 60);
		string fixSecondTime = secondTime.ToString();
		if (secondTime >= 0 && secondTime < 10)
			fixSecondTime = "0" + fixSecondTime;

		string showTime = string.Format("{0} : {1}", minuteTime, fixSecondTime);
		if (realTimeEvent != null)
			realTimeEvent(showTime);
	}
}
