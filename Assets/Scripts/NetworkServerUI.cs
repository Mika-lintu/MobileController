using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;



public class NetworkServerUI : MonoBehaviour
{
    //PlayerInput player;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    //PlayerStats player1;

    int playercount = 0;

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
       
        NetworkServer.Listen(2310);
        //NetworkServer.RegisterHandler(111, PlayerOne);
        //NetworkServer.RegisterHandler(222, PlayerTwo);
        //NetworkServer.RegisterHandler(333, PlayerThree);
        //NetworkServer.RegisterHandler(444, PlayerFour);

        //NetworkServer.RegisterHandler(999, NewPlayer);
        NetworkServer.RegisterHandler(999, RecieveMessage);
    }

    private void Update()
    {
        if (playercount != 4)
        {
            if (playercount < (NetworkServer.connections.Count - 1))
            {
                playercount = (NetworkServer.connections.Count - 1);

                switch (playercount)
                {
                    case 1:
                        player1.GetComponent<PlayerStats>().playerID = 1;
                        SendMessage(1, 0, 1);
                        break;
                    case 2:
                        player2.GetComponent<PlayerStats>().playerID = 2;
                        SendMessage(2, 0, 2);
                        break;
                    case 3:
                        player3.GetComponent<PlayerStats>().playerID = 3;
                        SendMessage(3, 0, 3);
                        break;
                    case 4:
                        player4.GetComponent<PlayerStats>().playerID = 4;
                        SendMessage(4, 0, 4);
                        break;
                    default:
                        break;
                }
            }
        }
    }


    private void RecieveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        int messageID;
        int playerIDNum;

        if (int.TryParse(deltas[0], out messageID)) { }
        if (int.TryParse(deltas[1], out playerIDNum)) { }

        if (messageID == 1) //Joystick Input message
        {
            JoystickInput(playerIDNum, deltas[2], deltas[3]);
        }
        else if (messageID == 2) // Button input message
        {
            ButtonInput(playerIDNum, deltas[2], deltas[3]);
        }
    }

    void JoystickInput(int playerID, string horizontal, string vertical)
    {
        PlayerStats player = GetPlayerStats(playerID);
        player.axisH = (Convert.ToSingle(horizontal));
        player.axisV = (Convert.ToSingle(vertical));
        print("Axis information H: " + horizontal + " and V: " + vertical);
    }

    void ButtonInput(int playerID, string pressed, string buttonPressed)
    {
        PlayerInput player = GetPlayerScript(playerID);
        int theBool;
        int buttonID;
        
        if (int.TryParse(pressed, out theBool)) { }
        
      
        if (int.TryParse(buttonPressed, out buttonID)) { }
              
        switch (buttonID)
                {
                    case 1:
                        if (theBool == 1)
                        {
                            print("Button 1 pressed");
                            player.ShootLeft(true);
                        }
                        else
                        {
                            print("Button 1 released");
                            player.ShootLeft(false);
                        }

                        break;
                    case 2:
                        if (theBool == 1)
                        {
                            print("Button 2 pressed");
                            player.ShootRight(true);
                        }
                        else
                        {
                            print("Button 2 released");
                            player.ShootRight(false);
                        }

                        break;
                    case 3:
                        if (theBool == 1)
                        {
                            print("Button 3 pressed");
                            player.SwapWeapons(true);
                        }
                        else
                        {
                            print("Button 3 released");
                            player.SwapWeapons(false);
                        }

                        break;

                    default:
                        break;
                }
    }

    PlayerInput GetPlayerScript(int id)
    {
        switch (id)
        {
            case 1:
                return player1.GetComponent<PlayerInput>();
            case 2:
                return player2.GetComponent<PlayerInput>();
            case 3:
                return player3.GetComponent<PlayerInput>();
            case 4:
                return player3.GetComponent<PlayerInput>();
            default:
                break;
        }
        return null;
    }

    PlayerStats GetPlayerStats(int id)
    {
        switch (id)
        {
            case 1:
                return player1.GetComponent<PlayerStats>();
            case 2:
                return player2.GetComponent<PlayerStats>();
            case 3:
                return player3.GetComponent<PlayerStats>();
            case 4:
                return player3.GetComponent<PlayerStats>();
            default:
                break;
        }
        return null;
    }
    /*
    // GET MESSAGE FROM CLIENT WITH FIRST ID
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

                player1.GetComponent<PlayerInput>().axisH = (Convert.ToSingle(deltas[1]));
                player1.GetComponent<PlayerStats>().axisV = (Convert.ToSingle(deltas[2]));
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
                            player1.GetComponent<PlayerInput>().ShootLeft(isPressed);
                            break;
                        case 2:
                            player1.GetComponent<PlayerInput>().ShootRight(isPressed);
                            break;
                        case 3:
                            player1.GetComponent<PlayerInput>().SwapWeapons(isPressed);
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
                player2.GetComponent<PlayerStats>().axisH = (Convert.ToSingle(deltas[1]));
                player2.GetComponent<PlayerStats>().axisV = (Convert.ToSingle(deltas[2]));
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
                            player2.GetComponent<PlayerInput>().ShootLeft(isPressed);
                            break;
                        case 2:
                            player2.GetComponent<PlayerInput>().ShootRight(isPressed);
                            break;
                        case 3:
                            player2.GetComponent<PlayerInput>().SwapWeapons(isPressed);
                            break;
                        default:
                            break;
                    }
            }
    }

    private void PlayerThree(NetworkMessage message)
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
                player3.GetComponent<PlayerStats>().axisH = (Convert.ToSingle(deltas[1]));
                player3.GetComponent<PlayerStats>().axisV = (Convert.ToSingle(deltas[2]));
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
                            player3.GetComponent<PlayerInput>().ShootLeft(isPressed);
                            break;
                        case 2:
                            player3.GetComponent<PlayerInput>().ShootRight(isPressed);
                            break;
                        case 3:
                            player3.GetComponent<PlayerInput>().SwapWeapons(isPressed);
                            break;
                        default:
                            break;
                    }
            }
    }

    private void PlayerFour(NetworkMessage message)
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
                player4.GetComponent<PlayerInput>().axisH = (Convert.ToSingle(deltas[1]));
                player4.GetComponent<PlayerInput>().axisV = (Convert.ToSingle(deltas[2]));
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
                            player4.GetComponent<PlayerInput>().ShootLeft(isPressed);
                            break;
                        case 2:
                            player4.GetComponent<PlayerInput>().ShootRight(isPressed);
                            break;
                        case 3:
                            player4.GetComponent<PlayerInput>().SwapWeapons(isPressed);
                            break;
                        default:
                            break;
                    }
            }
    }

    // SET CLIENT MESSAGE NUMBER

    private void NewPlayer(NetworkMessage message)
    {
        switch (playercount)
        {
            case 0:
                print("ups");
                break;
            case 1:
                SendMessage(1,0,1);
                player1.GetComponent<PlayerInput>().playerID = 1;
                break;
            case 2:
                SendMessage(2,0,2);
                player2.GetComponent<PlayerInput>().playerID = 2;
                break;
            case 3:
                SendMessage(3,0,3);
                player3.GetComponent<PlayerInput>().playerID = 3;
                break;
            case 4:
                SendMessage(4,0,4);
                player3.GetComponent<PlayerInput>().playerID = 4;
                break;

            default:
                
                break;
        }
        print(playercount);
        playercount++;         
    }*/

    // SEND MESSAGE TO SPESIFIC CLIENT
    public void SendMessage(int playerID, int msgID, int msgInfo)
    {
        StringMessage msg = new StringMessage();
        msg.value = msgID + "|" + msgInfo;
        print(playerID  +" " + msgID + " " + msgInfo);
        NetworkServer.SendToClient(NetworkServer.connections[playerID].connectionId, 999, msg);
    }

    // SEND TO ALL CLIENTS
    void SendToAllClients()
    {
        StringMessage msg = new StringMessage();
        msg.value = 1 + "";
        NetworkServer.SendToAll(999, msg);
    }

}
