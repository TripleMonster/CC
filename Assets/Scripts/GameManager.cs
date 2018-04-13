using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlaneMnager))]
public class GameManager : MonoSigleton<GameManager> {

    public PlaneMnager _PlaneMnager;
    public List<Transform> _Waypoints;

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
