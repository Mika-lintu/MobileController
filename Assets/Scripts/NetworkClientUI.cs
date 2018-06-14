using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkClientUI : MonoBehaviour {
    public string serverIP;
    public string portNumber;
    
    static NetworkClient client;


    static short messageNumber;

    private void OnGUI()
    {
        string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

       
        
        
        if (!client.isConnected)
        {
            serverIP = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20),serverIP,25);
            portNumber = GUI.TextField(new Rect(Screen.width - 110, 50, 100, 20), portNumber, 25);
            
            if (GUI.Button(new Rect(80, 10, 80, 50), "Player One"))
            {
                messageNumber = 111;
            }
         
            if (GUI.Button(new Rect(170, 10, 80, 50), "Player Two"))
            {
                messageNumber = 222;
            }

            if (GUI.Button(new Rect(10, 10, 60, 50), "Connect"))
            {
                Connect();
            }
            
        }
    }

    void Start ()
    {
        client = new NetworkClient();
	}

    void Connect()
    {
        int port;
        if (int.TryParse(portNumber, out port))
        client.Connect(serverIP, port);
    }

    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        //Message value always has 
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 1 + "|" + hDelta + "|" + vDelta;
            client.Send(messageNumber, msg);
        }
    }
    static public void SendButtonInfo(string name)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 2 +"|"+ name;
            client.Send(messageNumber, msg);
        }
    }

    void Update () {
		
	}
}
