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
		m_CountDownTimer.SetCountDownTime(100);
		m_CountDownTimer.intRealTimeEvent.AddListener(UpdateCountDownText);

	}
	
	void Update () 
	{
	}

	void UpdateCountDownText(int value)
	{
		_countDownText.text = "倒计时 : " + value.ToString();
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
