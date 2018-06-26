using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEventUtils;
using UnityEngine.UI;

public enum TTCountDownType { CD_TIME, CLOCK_TIME }

public class TTCountDownTimer : MonoBehaviour 
{
	[SerializeField] private TTCountDownType _currentCountDownType;
	[SerializeField] private Text _showTimeText;
	[HideInInspector] public bool isBeginTimer;

	private float countDownTime;
	private int intRealTime;      // 单位 秒
	private int m_speedFactor;
	public int speedFactor
	{
		get { return m_speedFactor; }
		set { m_speedFactor = value; }
	}

	void Start () { }
	
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
			
			if (_currentCountDownType == TTCountDownType.CD_TIME)
				CheckRealTime();
			
			if (_currentCountDownType == TTCountDownType.CLOCK_TIME)
				CheckFloatTime();
		}
	}

	public void InitCoutDownTimer(float cdTime, int cdSpeed=1)
	{
		countDownTime = cdTime;
		speedFactor = cdSpeed;

		if (_currentCountDownType == TTCountDownType.CD_TIME)
		{
			intRealTime = (int)countDownTime;
			CheckRealTime();
		}

		if (_currentCountDownType == TTCountDownType.CLOCK_TIME)
			CheckFloatTime();
	}

	void CheckRealTime()
	{
		int curTime = Mathf.CeilToInt(countDownTime);
		if (curTime <= intRealTime)
		{
			intRealTime = curTime;
			ShowTimeText(intRealTime.ToString());
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
		ShowTimeText(showTime);
	}

	void ShowTimeText(string timeText)
	{
		_showTimeText.text = timeText;
	}
}
