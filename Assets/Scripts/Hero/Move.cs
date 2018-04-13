using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    List<Transform> _WayPoints;

    Animator heroAnimator;
    UnityEngine.AI.NavMeshAgent heroAgent;

    private int currentPointIndex;
    private Vector3 targetPos;

    public void Init()
    {
        StartMove();
    }

    public void StartMove()
    {
        heroAnimator.SetBool("Run", true);
        if (heroAgent.isStopped)
            heroAgent.isStopped = false;
        currentPointIndex = FindNearestWaypointIndex();
        StartAutoMoveAlongWaypoint();
    }

    public void StopMove()
    {
        heroAgent.isStopped = true;
        heroAnimator.SetBool("Run", false);
    }

	private void Awake()
	{
        heroAnimator = GetComponent<Animator>();
        heroAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        _WayPoints = GameManager.Instance.GetWaypoints();
	}

	void Update () 
    {
        if (IsCheckedToMove())
            StartAutoMoveAlongWaypoint();
	}

    void StartAutoMoveAlongWaypoint()
    {
        if (currentPointIndex >= _WayPoints.Count)
            currentPointIndex = 0;

        targetPos = _WayPoints[currentPointIndex].position;
        heroAgent.destination = targetPos;
        currentPointIndex++;
    }

    int FindNearestWaypointIndex()
    {
        int index = 0;
        float lastDistance = 999;
        for (int i = 0; i < _WayPoints.Count; i++)
        {
            Vector3 waypoint = _WayPoints[i].position;
            float distance = Vector3.Distance(transform.position, waypoint);
            if (distance < lastDistance && i > currentPointIndex)
            {
                lastDistance = distance;
                index = i;
            }
        }

        return index;
    }

    bool IsCheckedToMove()
    {
        if (heroAgent.isStopped)
            return false;

        Vector2 pos1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 pos2 = new Vector2(targetPos.x, targetPos.z);

        if (Vector2.Distance(pos1, pos2) < 0.25f)
            return true;

        return false;
    }
}
