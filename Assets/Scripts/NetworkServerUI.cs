using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;



public class NetworkServerUI : MonoBehaviour {

    CrossPlatformInputManager.VirtualAxis virtualHAxisP1;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP1;

    CrossPlatformInputManager.VirtualAxis virtualHAxisP2;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP2;

    //CrossPlatformInputManager.VirtualAxis virtualHAxisP3;
    //CrossPlatformInputManager.VirtualAxis virtualVAxisP3;

    CarInput player;

    public CarInput player1;
    public CarInput player2;
    //public GameObject player3;


    List<string> players;

    public string newPort;


    public const short ClientMessageType = MsgType.Highest + 1;

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
        if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 30, 100, 60), "SendMessage"))
        {
           SendMessage();
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


        //virtualHAxisP3 = new CrossPlatformInputManager.VirtualAxis("Horizontal_P3");
        //CrossPlatformInputManager.RegisterVirtualAxis(virtualHAxisP3);
        //virtualVAxisP3 = new CrossPlatformInputManager.VirtualAxis("Vertical_P3");
        //CrossPlatformInputManager.RegisterVirtualAxis(virtualVAxisP3);

        NetworkServer.Listen(2310);
        NetworkServer.RegisterHandler(111, PlayerOne);
        NetworkServer.RegisterHandler(222, PlayerTwo);
        //NetworkServer.RegisterHandler(333, RecieveMessage);

        NetworkServer.RegisterHandler(999, NewPlayer);
    }

    /*private void RecieveMessage(NetworkMessage message)
    {
        short messageNum = message.msgType;
        int messageID;
        bool isPressed = false;
        int buttonID;

        switch (messageNum)
        {
            case 111:
                player = player1;
                print("Player One " + messageNum);
                break;

            case 222:
                player = player2;
                print("Player Two " + messageNum);
                break;

            case 333:
                print("No More Players");
                break;

            case 444:
                print("No More Players");
                break;

            case 555:
                print("No More Players");
                break;

            case 666:
                print("No More Players");
                break;

            case 777:
                print("No More Players");
                break;

            case 888:
                print("No More Players");
                break;

            case 999:
                print("No More Players");
                break;

            default:
                break;
        }

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        if (int.TryParse(deltas[0], out messageID)) { }

        if (messageID == 1)
        {
            virtualHAxisP1.Update(Convert.ToSingle(deltas[1]));
            virtualVAxisP1.Update(Convert.ToSingle(deltas[2]));

        }
        else if (messageID == 2)
        {
            int temp;
            if (int.TryParse(deltas[2], out temp))
                if (temp == 1)
                {
                    isPressed = true;
                }
                else
                {
                    isPressed = false;
                }

            if (int.TryParse(deltas[3], out buttonID))

                switch (buttonID)
                {
                    case 1:
                        player.Button1(isPressed);
                        break;

                    case 2:
                        player.Button2(isPressed);
                        break;

                    default:
                        break;
                }
        }

    }*/

    private void PlayerOne(NetworkMessage message)
    {
        
        int messageID;
        bool isPressed = false;
        int buttonID;

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        
        if (int.TryParse(deltas[0], out messageID))

            if (messageID == 1)
            {
                virtualHAxisP1.Update(Convert.ToSingle(deltas[1]));
                virtualVAxisP1.Update(Convert.ToSingle(deltas[2]));
                
            }
            else if (messageID == 2)
            {
                int temp;
                if (int.TryParse(deltas[2], out temp))
                    if (temp == 1)
                    {
                        isPressed = true;
                    }
                    else
                    {
                        isPressed = false;
                    }

                if (int.TryParse(deltas[3], out buttonID))

                    switch (buttonID)
                    {
                        case 1:
                            player1.Button1(isPressed);
                            break;
                        case 2:
                            player1.Button2(isPressed);
                            break;

                        default:
                            break;
                    }
            }
    }

    private void PlayerTwo(NetworkMessage message)
    {
        int messageID;
        bool isPressed = false;
        int buttonID;

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        if (int.TryParse(deltas[0], out messageID))


            if (messageID == 1)
            {
                virtualHAxisP2.Update(Convert.ToSingle(deltas[1]));
                virtualVAxisP2.Update(Convert.ToSingle(deltas[2]));
            }
            else if (messageID == 2)
            {
                int temp;
                if (int.TryParse(deltas[2], out temp))
                    if (temp == 1)
                    {
                        isPressed = true;
                    }
                    else
                    {
                        isPressed = false;
                    }

                if (int.TryParse(deltas[3], out buttonID))

                switch (buttonID)
                {
                    case 1:
                            player2.Button1(isPressed);
                        break;
                    case 2:
                            player2.Button2(isPressed);
                        break;

                    default:
                        break;
                }
            }
    }

    private void NewPlayer(NetworkMessage message)
    {
        /*
         Add player to list
         Give free message port to player
         Send information to client
         */
         //Network
    }

  
    void SendMessage()
    {
        StringMessage msg = new StringMessage();
        msg.value = "This is server, does client copy?";
        NetworkServer.SendToAll(MsgType.Highest + 1, msg);
        //NetworkServer.SendToClientOfPlayer(player3, MsgType.Highest + 1,msg);
        //NetworkClient.allClients[1];
    }
    

    void Update ()
    {
		
	}
}
