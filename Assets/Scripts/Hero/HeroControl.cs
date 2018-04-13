using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour 
{
    public List<Transform> _WayPoints;

    Animator heroAnimator;
    UnityEngine.AI.NavMeshAgent heroAgent;
    CapsuleCollider capsuleCollider;

    #region
    private float attackRange = 2f;
#endregion

    #region 自动寻路
    private int currentPointIndex;
    private Vector3 targetPos;
    #endregion

#region 找可攻击对象
    private bool IsStartToFind = true;
    private GameObject attackableTarget;
#endregion

    void Start () 
    {
        capsuleCollider.radius = attackRange;
	}

	private void Awake()
	{
        heroAnimator = GetComponent<Animator>();
        heroAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
	}

	void Update () 
    {
        if (IsCheckedToMove())
            StartAutoMoveAlongWaypoint();

        if (IsStartToFind)
            ToFindAttackableTarget();
	}

    void StartAutoMoveAlongWaypoint()
    {
        Debug.Log("index------" + currentPointIndex);
        if (currentPointIndex >= _WayPoints.Count)
            currentPointIndex = 0;
        
        targetPos = _WayPoints[currentPointIndex].position;
        ToMove();
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
            if (distance < lastDistance)
            {
                lastDistance = distance;
                index = i;
            }
        }
        Debug.Log("距离最近------index=" + index + "distance=" + lastDistance);
        return index;
    }

    void ToMove()
    {
        heroAnimator.SetBool("Run", true);
        heroAgent.destination = targetPos;
        heroAgent.isStopped = false;
    }

    void StopMove()
    {
        heroAgent.isStopped = true;
        heroAnimator.SetBool("Run", false);
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

    void ToFindAttackableTarget()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position, transform.position, attackRange, LayerMask.GetMask("EnemyLayer"));
        foreach(var item in colliders)
        {
            if (item == null)
                break;
            Debug.Log("找到了攻击目标:" + item.name);
            attackableTarget = item.gameObject;
            FoundAttackTarget();
            break;
        }
    }

    void AdjustRotation()
    {
        Vector3 direction = attackableTarget.transform.position - transform.position;
        direction.Normalize();
        float angle = 90 - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void FoundAttackTarget()
    {
        IsStartToFind = false;
        StopMove();
        AdjustRotation();
        //StartToAttackTarget();
    }

    void StartToAttackTarget()
    {
        //heroAnimator.SetTrigger("Attack");
        heroAnimator.SetBool("isAttacking", true);
    }

    public void Activate () 
    {
        heroAnimator.enabled = true;
        currentPointIndex = FindNearestWaypointIndex();
        StartAutoMoveAlongWaypoint();
    }

    public void InActivate () 
    {
        heroAnimator.enabled = false;
        gameObject.layer = 0;
    }
}