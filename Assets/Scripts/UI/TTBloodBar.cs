using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTBloodBar : MonoBehaviour 
{
	private Vector3 offset = new Vector3(0, 3, 0);
	private Slider bloodBarSlider;
	private Transform followTarget;

	private void Awake() 
	{
		bloodBarSlider = GetComponent<Slider>();	
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (followTarget != null)
		{
			transform.position = Camera.main.WorldToScreenPoint(followTarget.position + offset);
		}
	}

	public void SetFollowTarget(Transform target)
	{
		if (followTarget == null && target != null)
		{
			followTarget = target;
		} 
		else
		{
			followTarget = null;
		}
	}

	public void SetBloodVolume(float volume)
	{
		if (bloodBarSlider.value >= volume)
			bloodBarSlider.value -= volume;
		else
			bloodBarSlider.value = 0;
	}
}
