using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSigleton<GameManager> {

    public PlaneMnager _PlaneMnager;
    public List<Transform> _Waypoints;

    public TTMainThreadDispatcher mainThreadDispatcher;

    private void Awake()
    {
        mainThreadDispatcher = GetComponent<TTMainThreadDispatcher>();
    }

    void Start () 
    {
        
	}
	
	void Update () 
    {
		
	}

    public List<Transform> GetWaypoints()
    {
        return _Waypoints;
    }
}
