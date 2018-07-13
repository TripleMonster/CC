using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using DG.Tweening;
using TT;

public class TestHero : MonoBehaviour 
{
	private int port = 8123;
	private string ipStr = "127.0.0.1";
	public Canvas _animateCanvas;
	public Image _GoldIcon;

	public void TestSocket()
	{
		IPAddress ip = IPAddress.Parse(ipStr);
		IPEndPoint ip_end_point = new IPEndPoint(ip, port);
		Socket testSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		testSocket.Connect(ip_end_point);

		string testData = "wo shi ni yeye";
		testSocket.Send(System.Text.Encoding.Default.GetBytes(testData));
		testSocket.Close();
	}

	public void OnGetGold()
	{
		StartCoroutine(GoldAnimate(1));
	}

	IEnumerator GoldAnimate(int index)
	{
		for (int i = 0; i < 5; i++)
		{
			Image goldImg = Instantiate(_GoldIcon);
			goldImg.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
			goldImg.transform.position = new Vector3(500, 500, 0);
			goldImg.transform.parent = _animateCanvas.transform;
			goldImg.gameObject.SetActive(true);
			
			Sequence gSeq = DOTween.Sequence();
			Vector3 curPosition = goldImg.transform.position;
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
			gSeq.Append(goldImg.transform.DOMove(offset, 0.3f));
			gSeq.Join(goldImg.transform.DORotate(new Vector3(360, 360, 0), 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo));
			gSeq.Append(goldImg.transform.DOMove(new Vector3(0, 500 + 500, 0), 0.8f));

			yield return new WaitForSeconds(0.1f);
		}
	}
}
