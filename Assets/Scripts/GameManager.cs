using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSigleton<GameManager> {

    public PlaneMnager _PlaneMnager;
    public List<Transform> _Waypoints;
    public GameObject _UiCardButtions;
    public LoadingPanel _LoadingPanel;
    public MySlider _mySlider;

    [HideInInspector]public TTMainThreadDispatcher mainThreadDispatcher;


    private void Awake()
    {
        mainThreadDispatcher = GetComponent<TTMainThreadDispatcher>();
    }

    void Start () 
    {
        _LoadingPanel.isStartLoading = true;
	}
	float addTime = 0;
	void Update () 
    {
		if (addTime < 3)
        {
            addTime += Time.deltaTime;
        }
        else
        {
            _mySlider.value += 0.1f;
            addTime = 0;
        }
	}

    public void ActiveOrInActiveUiCardButtions(bool isActive)
    {
        _UiCardButtions.SetActive(isActive);
    }

    public List<Transform> GetWaypoints()
    {
        return _Waypoints;
    }
}
