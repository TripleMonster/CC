using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TT
{
	public enum ResourcesType { GOLD = 30, GEMS = 31, EXPERIENCE = 32} 

	public class TTCollectResourcesAnimation : MonoSigleton<TTCollectResourcesAnimation> 
	{
		[SerializeField] private Image _GoldIcon;
		[SerializeField] private Image _GemsIcon;
		[SerializeField] private Image _ExperienceIcon;

		ResourcesType currentType;
		Vector3 beginPosition;
		Vector3 endPosition;
		Action completed = null;
		public void PlayAnimation(ResourcesType type, Vector3 beginPosition, Vector3 endPosition, Action completed = null)
		{
			currentType = type;
			this.beginPosition = beginPosition;
			this.endPosition = endPosition;
			this.completed = completed;
			StartCoroutine(ReourcesAnimation());
		}

		IEnumerator ReourcesAnimation()
		{
			for (int i = 0; i < 5; i++)
			{
				Image resourcesImg = CreateResourcesImage();
				Sequence gSeq = DOTween.Sequence();
				Vector3 curPosition = resourcesImg.transform.position;
				Vector3 offset = new Vector3();

				int offsetX = 0, offsetY = 0;
				float factor = 3f;
				if (i == 0)
				{
					offsetX = 15; 
					offsetY = 10;
				}
				else if(i == 1)
				{
					offsetX = 15;
					offsetY = -10;
				}
				else if (i == 2)
				{
					offsetX = -15;
					offsetY = 10;
				}
				else if (i == 3)
				{
					offsetX = -15;
					offsetY = -10;
				}
				else if (i == 4)
				{
					offsetX = 0;
					offsetY = 20;
				}
				offset = curPosition + new Vector3(offsetX * factor, offsetY * factor, 0);
				gSeq.Append(resourcesImg.transform.DOMove(offset, 0.3f));
				gSeq.Join(resourcesImg.transform.DORotate(new Vector3(360, 360, 0), 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo));
				gSeq.Append(resourcesImg.transform.DOMove(this.endPosition, 0.8f));
				gSeq.AppendCallback( ()=> {
					if (completed != null)
						completed();
					Destroy(resourcesImg);
				});

				yield return new WaitForSeconds(0.1f);
			}
		}

		Image CreateResourcesImage()
		{
			Image resourcesImg = null;
			if (currentType == ResourcesType.GOLD)
				resourcesImg = Instantiate(_GoldIcon);
			else if (currentType == ResourcesType.GEMS)
				resourcesImg = Instantiate(_GemsIcon);
			else if (currentType == ResourcesType.EXPERIENCE)
				resourcesImg = Instantiate(_ExperienceIcon);
			
			resourcesImg.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
			resourcesImg.transform.position = this.beginPosition;
			resourcesImg.transform.parent = transform;
			resourcesImg.gameObject.SetActive(true);
			
			return resourcesImg;
		}
	}
}


