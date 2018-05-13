using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour {
	public Slider _loadingSlider;
	private int loadingStep;
	private bool beginLoading;

	void Start () 
	{
		
	}

	void Update () 
	{
		if (beginLoading)
			BeginLoading();
	}

	void BeginLoading()
	{
		
	}
}
