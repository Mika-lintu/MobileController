using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.PlayerConnection;
using UnityStandardAssets.CrossPlatformInput;



public class NetworkServerUI : MonoBehaviour {

    CrossPlatformInputManager.VirtualAxis virtualHAxisP1;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP1;

    CrossPlatformInputManager.VirtualAxis virtualHAxisP2;
    CrossPlatformInputManager.VirtualAxis virtualVAxisP2;

    //CrossPlatformInputManager.VirtualAxis virtualHAxisP3;
    //CrossPlatformInputManager.VirtualAxis virtualVAxisP3;

    //PlayerInput player;

    public PlayerInput player1;
    public PlayerInput player2;

    int playercount = 1;

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
        //if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 50, 100, 60), "UpdateClient"))
        //{

        //    //print(NetworkServer.connections[1]);
        //    SendClient();
        //}
        //if (GUI.Button(new Rect(Screen.width - 230, Screen.height - 50, 100, 60), "Plaa"))
        //{
        //    for (int i = 0; i < NetworkServer.connections.Count; i++)
        //    {
        //        print(NetworkServer.connections[i]);
        //    }

        //}
        //if (NetworkServer.connections.Count <= 2)
        //{
        //    GUI.Box(new Rect(30, 75, 200, 50), "");
        //    GUI.Label(new Rect(35, 80, 150, 20), "Joystick H: " + debugH);
        //    GUI.Label(new Rect(35, 100, 150, 20), "Joystick V: " + debugV);
        //}
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
                // virtualHAxisP1.Update(Convert.ToSingle(deltas[1]));
                // virtualVAxisP1.Update(Convert.ToSingle(deltas[2]));
                player1.axisH = (Convert.ToSingle(deltas[1]));
                player1.axisV = (Convert.ToSingle(deltas[2]));
        }
        else if (messageID == 2)
        {
             
                int theBool;
            if (int.TryParse(deltas[2], out theBool))

            if (theBool == 1)
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
                case 3:
                    player1.Button3(isPressed);
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
                //virtualHAxisP2.Update(Convert.ToSingle(deltas[1]));
                //virtualVAxisP2.Update(Convert.ToSingle(deltas[2]));
                player2.axisH = (Convert.ToSingle(deltas[1]));
                player2.axisV = (Convert.ToSingle(deltas[2]));
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
                    case 3:
                            player2.Button3(isPressed);
                        break;
                    default:
                        break;
                }
            }
    }

    private void NewPlayer(NetworkMessage message)
    {
        print("I happen");
                
        switch (playercount)
        {
            case 0:
                print("ups");
                break;
            case 1:
                SendMessage(1,0,1);
                player1.playerID = 1;
                break; 
            case 2:
                SendMessage(2,0,2);
                player2.playerID = 2;
                break;
            default:
                
                break;
        }
        print(playercount);
        playercount++;         
    }

    public void SendMessage(int playerID, int msgID, int msgInfo)
    {
        StringMessage msg = new StringMessage();
        msg.value = msgID + "|" + msgInfo;
        NetworkServer.SendToClient(NetworkServer.connections[playerID].connectionId, 999, msg);
    }

    void SendClient()
    {
        StringMessage msg = new StringMessage();
        msg.value = 1 + "";
        NetworkServer.SendToAll(987, msg);
        
    }

}
