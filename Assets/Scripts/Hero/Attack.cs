using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventUtils;

public class Attack : MonoBehaviour 
{
    [HideInInspector] public UEvent foundTargetEvent;
    [HideInInspector] public UEvent lostTargetEvent;

    Animator heroAnimator;

    private bool IsStartToFind;
    private GameObject attackableTarget;
    private float attackRange;

    public void Init()
    {
        IsStartToFind = true;
        attackRange = 2f;
        foundTargetEvent = new UEvent();
        lostTargetEvent = new UEvent();
    }

	private void Awake()
	{
        heroAnimator = GetComponent<Animator>();
	}

	void Update () 
    {
        if (IsStartToFind)
            ToFindAttackableTarget();
	}

    void ToFindAttackableTarget()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position, transform.position, attackRange, LayerMask.GetMask("EnemyLayer"));
        foreach (var item in colliders)
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

        foundTargetEvent.Invoke();

        AdjustRotation();

        StartCoroutine(StartToAttackTarget());
    }

    void LostAttackTarget()
    {
        heroAnimator.SetBool("isAttacking", false);
        IsStartToFind = true;
        Destroy(attackableTarget);
        lostTargetEvent.Invoke();
    }

    IEnumerator StartToAttackTarget()
    {
        heroAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(3.0f);
        LostAttackTarget();
    }
}
