using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using TT;

public class BattleUITop : MonoBehaviour 
{
	#region level
	[SerializeField] private Slider _ExperienceSlider;
	[SerializeField] private Text _ExperienceNum;
	[SerializeField] private TextMeshProUGUI _LevelNum;
	#endregion

	#region gold and gems
	[SerializeField] private Image _GoldIcon;
	[SerializeField] private Image _GemsIcon;
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
		TTCollectResourcesAnimation.Instance.PlayAnimation(ResourcesType.GOLD, new Vector3(500, 500, 0), _GoldIcon.transform.position, ()=>{
			NumberScrollAnimation(oldGold, newGold, _GoldNum);
		});
	}

	void UpdateGems()
	{
		int oldGems = Convert.ToInt32(_GemsNum.text);
		int newGmes = userProfile.userGems;
		TTCollectResourcesAnimation.Instance.PlayAnimation(ResourcesType.GEMS, new Vector3(500, 500, 0), _GemsIcon.transform.position, ()=> {
			NumberScrollAnimation(oldGems, newGmes, _GemsNum);
		});
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
		DataManager.Instance.ChangeGoldCount();
	}

	public void OnBuyGems()
	{
		DataManager.Instance.ChangeGemsCount();
	}
}
