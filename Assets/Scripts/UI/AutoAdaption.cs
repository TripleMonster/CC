using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAdaption : MonoBehaviour {

	[SerializeField] private RectTransform _adaptTarget;
	[SerializeField] private float _adaptLength;

	RectTransform rectTransform;

	void Start () 
	{
		float ratio = _adaptTarget.rect.width / _adaptLength;
		rectTransform = GetComponent<RectTransform>();
		float newX = rectTransform.localPosition.x * ratio;
		rectTransform.localPosition = new Vector3(newX, 0, 0);
	}
}
