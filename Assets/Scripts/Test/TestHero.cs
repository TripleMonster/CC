using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class TestHero : MonoBehaviour 
{
	private int port = 8123;
	private string ipStr = "127.0.0.1";
	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

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
}
