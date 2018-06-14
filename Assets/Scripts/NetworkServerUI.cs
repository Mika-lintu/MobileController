using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


public class NetworkServerUI : MonoBehaviour {

    CrossPlatformInputManager.VirtualAxis virtualHAxisP1;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP1;

    CrossPlatformInputManager.VirtualAxis virtualHAxisP2;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP2;


    string horizontalAxisName = "Horizontal";
    string verticalAxisName = "Vertical";

    public CarInput player1;
    public CarInput player2;

    public string newPort;

    private void OnGUI()
    {
        string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);
        newPort = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20), newPort, 25);
        if (GUI.Button(new Rect(Screen.width - 110, 30, 100, 60), "Update Port"))
        {
            int portNumber;
            if (int.TryParse(newPort, out portNumber))
                NetworkServer.Listen(portNumber);
        }
    }

    void Start ()
    {
        //Player 1 virtual axises
        virtualHAxisP1 = new CrossPlatformInputManager.VirtualAxis("Horizontal");
        CrossPlatformInputManager.RegisterVirtualAxis(virtualHAxisP1);
        virtualVAxisP1 = new CrossPlatformInputManager.VirtualAxis("Vertical");
        CrossPlatformInputManager.RegisterVirtualAxis(virtualVAxisP1);
        //Player 2 virtual axises
        virtualHAxisP2 = new CrossPlatformInputManager.VirtualAxis("Horizontal_P2");
        CrossPlatformInputManager.RegisterVirtualAxis(virtualHAxisP2);
        virtualVAxisP2 = new CrossPlatformInputManager.VirtualAxis("Vertical_P2");
        CrossPlatformInputManager.RegisterVirtualAxis(virtualVAxisP2);

        NetworkServer.Listen(2310);
        NetworkServer.RegisterHandler(111, PlayerOne);
        NetworkServer.RegisterHandler(222, PlayerTwo);
       
    }
	private void PlayerOne(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        int messageID;
        if (int.TryParse(deltas[0], out messageID))

            if (messageID == 1)
            {
                virtualHAxisP1.Update(Convert.ToSingle(deltas[1]));
                virtualVAxisP1.Update(Convert.ToSingle(deltas[2]));
                
            }
            else if (messageID == 2)
            {
                player1.Button1();
            }
    }

    private void PlayerTwo(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        int messageID;
        if (int.TryParse(deltas[0], out messageID))

            if (messageID == 1)
            {
                virtualHAxisP2.Update(Convert.ToSingle(deltas[1]));
                virtualVAxisP2.Update(Convert.ToSingle(deltas[2]));
                
            }
            else if(messageID == 2)
            {
                player2.Button1();
            }
    }

	
	void Update ()
    {
		
	}
}
