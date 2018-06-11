using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Attack))]
public class HeroControl : MonoBehaviour 
{
    CapsuleCollider capsuleCollider;
    Move heroMove;
    Attack heroAttack;
    TTBloodBar bloodBar;

    float addTime=0;
	private void Awake()
	{
        capsuleCollider = GetComponent<CapsuleCollider>();
        heroMove = GetComponent<Move>();
        heroAttack = GetComponent<Attack>();
	}

    private void Update() 
    {
        if (addTime < 10)
        {
            addTime += Time.deltaTime;
        }
        else
        {
            bloodBar.SetBloodVolume(0.1f);
            addTime = 0;
        }
    }

    public void Init()
    {
        Debug.Log("hero control position : " + transform.position);
        bloodBar = TTPoolsManager.Instance.GetBloodBarPrefabFromPool("TTBloodBar").GetComponent<TTBloodBar>();
        bloodBar.SetFollowTarget(transform);
        gameObject.layer = 0;
        StartCoroutine(DeployHero());
    }

    void OnFoundTarget()
    {
        heroMove.StopMove();
    }

    void OnLostTarget()
    {
        heroMove.StartMove();
    }

    IEnumerator DeployHero()
    {
        yield return new WaitForSeconds(1.0f);
        heroMove.Init();
        heroAttack.Init();

        heroAttack.foundTargetEvent.AddListener(OnFoundTarget);
        heroAttack.lostTargetEvent.AddListener(OnLostTarget);
    }

    private void OnDestroy() 
    {
         //bloodBar.SetFollowTarget(null);   
    }
}