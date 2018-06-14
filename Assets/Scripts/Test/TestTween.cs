using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TestTween : MonoBehaviour {

	Image _testImage;
	RectTransform _testRect;
	float offset = 0;

	void Start () 
	{
		_testImage = GetComponent<Image>();
		_testRect = _testImage.rectTransform;
	}
	
	void Update () 
	{
		StartCoroutine(Testxxx());	
	}

	IEnumerator Testxxx()
	{
		yield return new WaitForSeconds(2.0f); 
	}
}
