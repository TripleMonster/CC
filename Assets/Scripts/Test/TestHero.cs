using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHero : MonoBehaviour 
{
	public Text _countDownText;
	TTCountDownTimer m_CountDownTimer;

	void Start () 
	{
		m_CountDownTimer = GetComponent<TTCountDownTimer>();
		m_CountDownTimer.InitCoutDownTimer(TTCountDownType.FLOAT_TIME, 180, UpdateCountDownText);
	}
	
	void Update () 
	{
	}

	void UpdateCountDownText(string value)
	{
		_countDownText.text = "倒计时 : " + value;
	}

	public void BeginCountDown()
	{
		if (m_CountDownTimer)
			m_CountDownTimer.isBeginTimer = true;
	}

	public void AddCountDownSpeed()
	{
		if (m_CountDownTimer)
			m_CountDownTimer.speedFactor += 1;
	}
}
