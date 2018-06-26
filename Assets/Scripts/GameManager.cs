using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSigleton<GameManager> {

    public PlaneMnager _PlaneMnager;
    public List<Transform> _Waypoints;
    public GameObject _UICardButtions;
    public GameObject _UIHolyWater;
    public LoadingPanel _LoadingPanel;
    public TTSectionSlider _mySlider;
    public TTCountDownTimer _countDownTimer;

    [HideInInspector]public TTMainThreadDispatcher mainThreadDispatcher;


    private void Awake()
    {
        mainThreadDispatcher = GetComponent<TTMainThreadDispatcher>();
    }

    void Start () 
    {
        _LoadingPanel.isStartLoading = true;
        _countDownTimer.InitCoutDownTimer(180);
	}

	void Update () 
    {
		
	}

    int slideSpeed = 0;
    private void FixedUpdate()
    {
        if (slideSpeed == 20)
        {
            _mySlider.value += 0.01f;
            slideSpeed = 0;
        }
        else
        {
            float deltaTime = Time.deltaTime * 100;
            slideSpeed += (int)deltaTime;
        }
    }

    public void ActiveOrInActiveUiCardButtions(bool isActive)
    {
        _UICardButtions.SetActive(isActive);
        _UIHolyWater.SetActive(isActive);
        _countDownTimer.gameObject.SetActive(isActive);
        _countDownTimer.isBeginTimer = true;
    }

    public List<Transform> GetWaypoints()
    {
        return _Waypoints;
    }
}
