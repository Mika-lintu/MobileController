using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class NetworkClientUI : MonoBehaviour
{

    public string serverIP;
    public string portNumber;
    static string ipaddress;
    
    static NetworkClient client;
    static UIManager uIController;

    string serverMessage;

    static short messageNumber = 999;
    
    static bool playerID = false;
    static int clientID;

    private void OnGUI()
    {
        ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

        GUI.Box(new Rect(150, Screen.height - 50, 100, 50), "Server message");
        GUI.Label(new Rect(150, Screen.height - 30, 300, 20), serverMessage );
        
        if (!client.isConnected)
        {
            serverIP = GUI.TextField(new Rect(Screen.width - 110, 10, 100, 20),serverIP,25);
            portNumber = GUI.TextField(new Rect(Screen.width - 110, 50, 100, 20), portNumber, 25);
            if (GUI.Button(new Rect(10, 10, 60, 50), "Connect"))
            {
                Connect();                
            }
        }
         
    }

    void Start ()
    {
        uIController = GetComponent<UIManager>();
        client = new NetworkClient();
        client.RegisterHandler(999, GetMessageFromServer);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad0))
        {

        }
    }

    void Connect()
    {
        int port;
        if (int.TryParse(portNumber, out port))
        client.Connect(serverIP, port);
    }

    public void FirstMessage(string number)
    {
        if (client.isConnected)
        {

            if (int.TryParse(number, out clientID)) { }
            serverMessage = clientID+"";
            //StringMessage msg = new StringMessage();
            //msg.value = ipaddress;
            //client.Send(999, msg);
            //playerID = true;
        }
    }

    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = 1 + "|" + clientID + "|" + hDelta + "|" + vDelta;
            client.Send(messageNumber, msg);
        }
    }

    static public void SendButtonInfo(int pressed, int buttonID)
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
            msg.value = 2 + "|" + clientID + "|" + pressed + "|" + buttonID;
            client.Send(messageNumber, msg);
        
    }

    public void GetMessageFromServer(NetworkMessage message)
    {
        int msgID;
        int msgInfo;

        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
        //serverMessage = deltas[0];

        if (int.TryParse(deltas[0], out msgID)) { }
        if (int.TryParse(deltas[1], out msgInfo)) { }
        if (msgID == 0)
        {
            FirstMessage(deltas[1]);
        }
        else if (msgID == 1)
        {
            uIController.ResetUI(msgInfo);
        }
        else if (msgID == 2)
        {
            uIController.RemoveHealth(msgInfo);
        }
        else if (msgID == 3)
        {
            uIController.RemoveAmmo(msgInfo);
            serverMessage = "Remove ammo";
        }
        else if (msgID == 4)
        {
            uIController.AddAmmo(msgInfo);
        }
        else if (msgID == 5)
        {
            uIController.RemoveMine(msgInfo);
            //serverMessage = "Remove mine";
        }
        else if (msgID == 6)
        {
            uIController.AddMine(msgInfo);
        }
        else if (msgID == 7)
        {
            serverMessage = "" + msgInfo;
        }
    }

}
