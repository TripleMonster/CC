using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleUITop : MonoBehaviour 
{
	#region level
	[SerializeField] private Slider _ExperienceSlider;
	[SerializeField] private Text _ExperienceNum;
	[SerializeField] private TextMeshProUGUI _LevelNum;
	[SerializeField] private Image _GoldIcon;
	#endregion

	#region gold and gems
	[SerializeField] private Text _GoldNum;
	[SerializeField] private Text _GemsNum;
	#endregion

	private UserProfile userProfile;

	private void Start() 
	{
		
	}

	public void UpdateUI(int type)
	{
		userProfile = DataManager.Instance.GetUserProfile();
		switch (type)
		{
			case 1:
				UpdateLevel();
				break;
			case 2:
				UpdateGold();
				break;
			case 3:
				UpdateGems();
				break;
		}
	}

	void UpdateLevel()
	{
		_LevelNum.text = userProfile.userLevel.ToString();
		_ExperienceNum.text = userProfile.userExperience.ToString() + "/800000";
		_ExperienceSlider.value = userProfile.userExperience / 80000;
	}

	void UpdateGold()
	{
		int oldGold = Convert.ToInt32(_GoldNum.text);
		int newGold = userProfile.userGold;
		NumberScrollAnimation(oldGold, newGold, _GoldNum);
	}

	void UpdateGems()
	{
		int oldGems = Convert.ToInt32(_GemsNum.text);
		int newGmes = userProfile.userGems;
		NumberScrollAnimation(oldGems, newGmes, _GemsNum);
	}

	void NumberScrollAnimation(int oldNumber, int newNumber, Text numberText)
	{
		Sequence scrollSeq = DOTween.Sequence();
		scrollSeq.SetAutoKill(false);
		scrollSeq.Append(DOTween.To(delegate(float value){
			var temp = Math.Floor(value);
			numberText.text = temp.ToString();
		}, (float)oldNumber, (float)newNumber, 0.4f));
		
	}

	public void OnBuyGold()
	{
		// DataManager.Instance.ChangeGoldCount();
		Canvas canvasObj = GameObject.FindObjectOfType<Canvas>();
		for (int i = 0; i < 5; i++)
		{
			Image goldImg = Instantiate(_GoldIcon);
			goldImg.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
			goldImg.transform.position = new Vector3(500, 500, 0);
			goldImg.transform.parent = canvasObj.transform;
			
			Sequence gSeq = DOTween.Sequence();
			Vector3 offset = new Vector3();
			if (i < 2)
			{
				offset = goldImg.transform.position + new Vector3(100, 50*(i+1), 0);
			}
			else
			{
				offset = goldImg.transform.position + new Vector3(-100, 50*(i-1), 0);
			}
			
			gSeq.Append(goldImg.transform.DOMove(offset, 1f));
			gSeq.Join(goldImg.transform.DORotate(new Vector3(0, 360, 0), 1f).SetRelative().SetLoops(-1, LoopType.Yoyo));
			//gSeq.Append(goldImg.transform.DOMove(new Vector3(0, 500 + 500, 0), 0.8f));
		}
	}

	public void OnBuyGems()
	{
		DataManager.Instance.ChangeGemsCount();
	}
}
