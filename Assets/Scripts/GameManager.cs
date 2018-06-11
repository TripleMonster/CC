using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSigleton<GameManager> {

    public PlaneMnager _PlaneMnager;
    public List<Transform> _Waypoints;
    public GameObject _UiCardButtions;
    public LoadingPanel _LoadingPanel;

    [HideInInspector]public TTMainThreadDispatcher mainThreadDispatcher;


    private void Awake()
    {
        mainThreadDispatcher = GetComponent<TTMainThreadDispatcher>();
    }

    void Start () 
    {
        _LoadingPanel.isStartLoading = true;
	}
	
	void Update () 
    {
		
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
