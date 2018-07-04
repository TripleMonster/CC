using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanle : MonoBehaviour 
{
	[SerializeField] private BattleUITop _UITop;
	[SerializeField] private GameObject _UICenter;
	[SerializeField] private GameObject _UIBottom;

	void Start () 
	{
		DataManager.Instance.SetUserProfile();
		RegiserDataToUIEvent();
	}
	
	void Update () 
	{
		
	}

	void RegiserDataToUIEvent()
	{
		DataManager.Instance.userProfileEvent.AddListener(_UITop.UpdateUI);
	}
}
