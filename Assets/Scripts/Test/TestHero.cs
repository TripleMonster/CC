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
			Vector3 offset2 = new Vector3();

			int random = TTRandom.BetterRandom(1, 5);
			Debug.Log("random--------------" + random);
			offset = curPosition + new Vector3(20*(random-2), 20*(random+1), 0);
			offset2 = curPosition + new Vector3(10*(random-2), -10*(random+1), 0);
			gSeq.Append(goldImg.transform.DOMove(offset2, 0.3f));
			gSeq.Append(goldImg.transform.DOMove(offset, 0.3f));
			gSeq.Join(goldImg.transform.DORotate(new Vector3(360, 360, 0), 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo));
			gSeq.Append(goldImg.transform.DOMove(new Vector3(0, 500 + 500, 0), 0.8f));

			yield return new WaitForSeconds(0.1f);
		}
	}
}
