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

    void Start () 
    {
        
	}

	private void Awake()
	{
        capsuleCollider = GetComponent<CapsuleCollider>();
        heroMove = GetComponent<Move>();
        heroAttack = GetComponent<Attack>();
	}

	void Update () 
    {
        
	}

    public void Activate () 
    {
        heroMove.Init();
        heroAttack.Init();

        heroAttack.foundTargetEvent.AddListener(OnFoundTarget);
        heroAttack.lostTargetEvent.AddListener(OnLostTarget);
    }

    public void InActivate () 
    {
        gameObject.layer = 0;
    }

    void OnFoundTarget()
    {
        heroMove.StopMove();
    }

    void OnLostTarget()
    {
        heroMove.StartMove();
    }
}