using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUITop : MonoBehaviour 
{
	#region level
	[SerializeField] private Slider _ExperienceSlider;
	[SerializeField] private Text _ExperienceNum;
	[SerializeField] private TextMeshProUGUI _LevelNum;
	#endregion

	#region gold and gems
	[SerializeField] private Text _GoldNum;
	[SerializeField] private Text _GemsNum;
	#endregion

	private void Start() 
	{
		UpdateLevel();	
	}

	public void UpdateUI(int type)
	{
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
		UserProfile userProfile = DataManager.Instance.GetUserProfile();
		_LevelNum.text = userProfile.userLevel.ToString();
		_ExperienceNum.text = userProfile.userExperience.ToString() + "/800000";
		_ExperienceSlider.value = userProfile.userExperience / 80000;
	}

	void UpdateGold()
	{
		_GoldNum.text = DataManager.Instance.GetUserProfile().userGold.ToString();
	}

	void UpdateGems()
	{
		_GemsNum.text = DataManager.Instance.GetUserProfile().userGems.ToString();
	}

	public void OnBuyGold()
	{

	}

	public void OnBuyGems()
	{

	}
}
