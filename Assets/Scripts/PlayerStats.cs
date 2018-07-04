using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int playerID;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float axisH;
    public float axisV;

    public int mineCount = 3;
    public int shellCount = 3;
    
    NetworkServerUI server;

    private void Awake()
    {
        server = GameObject.Find("NetworkManager").GetComponent<NetworkServerUI>();
    }

    public void HealthMessage(int dmg, bool remove)
    {
        if (remove)
        {
            server.SendMessage(playerID, 2, dmg);
        }
        else if (!remove)
        {
            server.SendMessage(playerID, 1, 0);
            shellCount = 3;
            mineCount = 3;
        }
    }

    public void AmmoMessage(int amount, bool remove, string type)
    {
        if (remove)
        {   if (type == "Shell")
            {
                shellCount--;
                server.SendMessage(playerID, 3, amount);
            }
            else if (type == "Mine")
            {
                mineCount--;
                server.SendMessage(playerID, 5, amount);
            }
        }
        else if (!remove)//Meaning ADD
        {
            if (type == "Shell")
            {
                shellCount++;
                server.SendMessage(playerID, 4, amount);
            }
            else if (type == "Mine")
            {
                mineCount++;
                server.SendMessage(playerID, 6, amount);
            }
            
        }
    }
    public void AddPointMessage(int amount)
    {
        server.SendMessage(playerID, 7, amount);
    }

}