using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.IO;
using System.Threading;


public class TCP : MonoBehaviour 
{   
	string ip_address  = "127.0.0.1";
	int port = 9999;
	
	Thread listen_thread;
	TcpListener tcp_listener;
	Thread clientThread;
	TcpClient tcp_client;
	bool isTrue = true;
	
	// Use this for initialization
	void Start () 
	{
		IPAddress ip_addy = IPAddress.Parse(ip_address);
		tcp_listener = new TcpListener(ip_addy, port);
		listen_thread = new Thread(new ThreadStart(ListenForClients));
		listen_thread.Start();
		
		
		Debug.Log("start thread");
		
	}
	
	private void ListenForClients()
	{
		this.tcp_listener.Start();
		
		while(isTrue == true)   
		{
			//blocks until a client has connected to the server
			TcpClient client = this.tcp_listener.AcceptTcpClient();
			
			//create a thread to handle communication 
			//with connected client
			clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
			clientThread.Start(client);
			
			
			Debug.Log("Got client " + client);
			
		}
	}
	
	private void HandleClientComm(object client)
	{
		tcp_client = (TcpClient)client;
		NetworkStream client_stream = tcp_client.GetStream();
		
		
		byte[] message = new byte[4096];
		int bytes_read;
		
		while(isTrue == true)
		{
			bytes_read = 0;
			
			try
			{
				//blocks until a client sends a message
				bytes_read = client_stream.Read(message, 0, 4096);
				//Debug.Log(message);
				
			}
			catch (Exception e)
			{
				//a socket error has occured
				Debug.Log(e.Message);
				break;
			}
			
			if(bytes_read == 0)
			{
				//client has disconnected
				Debug.Log("Disconnected");
				tcp_client.Close();
				break;
			}
			
			ASCIIEncoding encoder = new ASCIIEncoding();
			Debug.Log(encoder.GetString(message,0,bytes_read));
			
			
		}
		
		if(isTrue == false)
		{
			tcp_client.Close();
			Debug.Log("closing tcp client");
		}
		
	}
	
	void OnApplicationQuit()
	{
		try
		{
			tcp_client.Close();
			isTrue = false;
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
		
		// You must close the tcp listener
		try
		{
			tcp_listener.Stop();
			isTrue = false;
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
	}

}