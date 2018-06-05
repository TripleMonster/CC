using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTMainThreadDispatcher : MonoBehaviour
{
	[HideInInspector] private List<IEnumerator> coroutines = new List<IEnumerator>();
	private object lockObj = new object();

	public void Commit(IEnumerator iEnum)
	{
		lock(lockObj)
		{
			coroutines.Add(iEnum);
		}
	}

	public void Commit(params IEnumerator[] iEnums)
	{
		
	}

	private IEnumerator CombineCoroutines(IEnumerator[] iEnums)
	{
		var index = 0;
		while (index < iEnums.Length)
		{
			yield return iEnums[index];
			index++;
		}
	}


	void Update () 
	{
		if (0 < coroutines.Count)
		{
			lock(lockObj)
			{
				var commitingList = new List<IEnumerator>(coroutines);
				coroutines.Clear();

				foreach (var coroutine in commitingList)
				{
					StartCoroutine(coroutine);
				}
			}
		}
	}

	private void OnApplicationQuit()
	{
		Destroy(gameObject);
	}

	public void OnDestroy()
	{
		GameObject.Destroy(gameObject);
	}
}
