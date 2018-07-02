using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class NetworkClientUI : MonoBehaviour {

    public string serverIP;
    public string portNumber;
    static string ipaddress;
    
    static NetworkClient client;
    static UIManager uIController;

    string serverMessage;

    static short messageNumber = 999;
    
    static bool playerID = false;
    public GameObject testObject;

    private void OnGUI()
    {
        ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

        GUI.Box(new Rect(150, Screen.height - 50, 100, 50), "Server message");
        GUI.Label(new Rect(150, Screen.height - 30, 300, 20), serverMessage);
        
        if (!client.isConnected)
        {
            serverIP = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20),serverIP,25);
            portNumber = GUI.TextField(new Rect(Screen.width - 110, 50, 100, 20), portNumber, 25);
            if (GUI.Button(new Rect(10, 10, 60, 50), "Connect"))
            {
                Connect();                
            }
        }
        else if (client.isConnected)
        {
            if (!playerID)
            {
                if (GUI.Button(new Rect(10, 10, 60, 50), "Start"))
                {
                    FirstMessage();
                }
            }
        }
    }

    void Start ()
    {
        uIController = GetComponent<UIManager>();
        client = new NetworkClient();
        client.RegisterHandler(999, GetMessageNumber);
        client.RegisterHandler(987, UpdateController);
    }

    void Connect()
    {
        int port;
        if (int.TryParse(portNumber, out port))
        client.Connect(serverIP, port);
    }

    static public void FirstMessage()
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = ipaddress;
            client.Send(999, msg);
            playerID = true;
        }
    }

    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 1 + "|" + hDelta + "|" + vDelta;
            client.Send(messageNumber, msg);
        }
    }

    static public void SendButtonInfo(string name, int pressed, int buttonID)
    {
        if (pressed == 1 && buttonID == 3)
        {
            uIController.SwapWeapons(buttonID);
        }
        if (pressed == 1 && !uIController.cooldownActive && buttonID != 3)
        {
            uIController.ActivateCooldown(buttonID);
        }
        if (client.isConnected)
        {
            if (pressed == 1 && buttonID == 3)
            {
                uIController.SwapWeapons(buttonID);
            }
            if (pressed == 1 && !uIController.cooldownActive && buttonID!=3)
            {
                uIController.ActivateCooldown(buttonID);
            }
            
            StringMessage msg = new StringMessage();
            msg.value = 2 + "|" + name + "|" + pressed + "|" + buttonID;
            client.Send(messageNumber, msg);
        }
    }

    public void GetMessageNumber(NetworkMessage message)
    {
        int msgID;
        int msgInfo;

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        serverMessage = deltas[0];

        if (int.TryParse(deltas[0], out msgID)) { }
        if (int.TryParse(deltas[1], out msgInfo)) { }
            if (msgID == 0)
            {
                    switch (msgInfo)
                    {
                        case 1:
                            messageNumber = 111;
                            client.RegisterHandler(977, UpdateController);
                            break;
                        case 2:
                            messageNumber = 222;
                            break;
                        default:
                            break;
                    }
            }
        else if(msgID == 1)
        {
            uIController.RemoveHealth(msgInfo);
        }
        else if(msgID == 2)
        {
            uIController.ResetUI(msgInfo);
        }
    }

    void UpdateController(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        int temp;
        if (int.TryParse(deltas[0], out temp)) { }
        if(temp == 1)
        {
            testObject.SetActive(true);
        }
    }

    void UpdateControllerUI(NetworkMessage message)
    {
        int UIType;

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

        if (int.TryParse(deltas[0], out UIType)) { }
        if (UIType == 1)
        {
            testObject.SetActive(true);
        }
        else if (UIType == 2)
        {

        }
        else if (UIType == 3)
        {

        }
    }
}
