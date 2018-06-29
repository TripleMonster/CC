using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChallengerEffect : MonoBehaviour 
{
	float moveOffset = 10;
	float positionY;
	bool beginMove;
	void Start () 
	{
		positionY = transform.position.y;
		beginMove = true;
	}
	
	void Update () 
	{
		if (beginMove)
		{
			beginMove = false;
			transform.DOMoveY(positionY + moveOffset, 2.5f).OnComplete(RevertDirection);
		}
	}

	void RevertDirection()
	{
		moveOffset = -moveOffset;
		beginMove = true;
	}
}
