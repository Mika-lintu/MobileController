using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput: MonoBehaviour {

    public int playerID;
    

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public string axisNameV;
    public string axisNameH;

    public float axisH;
    public float axisV;
    
    public Transform fireRight;
    public Transform fireLeft;
    public NetworkServerUI server;
    private string shootMode = "short";

    void Update () {

        //float translation = CrossPlatformInputManager.GetAxis(axisNameV) * speed * Time.deltaTime;
        //transform.Translate(0, 0, translation);

        //float rotation = CrossPlatformInputManager.GetAxis(axisNameH) * rotationSpeed;

        float rotation = axisH * rotationSpeed;
        rotation *= Time.deltaTime;

        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.Rotate(0, rotation, 0);


// TEST
        if (Input.GetKeyDown(KeyCode.A))
        {
            Button1(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Button2(true);
        }

        // switch shooting mode with S
        if (Input.GetKeyDown(KeyCode.S))
        {
            Button3(true);
        }
//
    }

    // shoot left
    public void Button1(bool pressed)
    {
        if (pressed)
        {
            print("Button 1 was pressed");

            if (shootMode == "short")
            {
                gameObject.GetComponent<PlayerShooting>().ShortRangeShoot(fireLeft);
            }

            else if (shootMode == "long")
            {
                gameObject.GetComponent<PlayerShooting>().LongRangeShoot(fireLeft);
            }
            
            else if (shootMode == "seamine")
            {
                gameObject.GetComponent<PlayerShooting>().Seamine();
            }
        }
        else
        {
            print("Button 1 was released");
        }
    }

    // shoot right
    public void Button2(bool pressed)
    {
        if (pressed)
        {
            print("Button 2 was pressed");

            if (shootMode == "short")
            {
                gameObject.GetComponent<PlayerShooting>().ShortRangeShoot(fireRight);
            }
            
            else if (shootMode == "long")
            {
                gameObject.GetComponent<PlayerShooting>().LongRangeShoot(fireRight);
            }

            else if (shootMode == "seamine")
            {
                gameObject.GetComponent<PlayerShooting>().Seamine();
            }
        }
        else
        {
            print("Button 2 was released");
        }
    }

    // swap weapons
    public void Button3(bool pressed)
    {
        if (pressed)
        {
            print("Button 3 was pressed");

            if (shootMode == "short")
            {
                shootMode = "long";
            }
            else if (shootMode == "long")
            {
                shootMode = "seamine";
            }
            else
            {
                shootMode = "short";
            }
        }
        else
        {
            print("Button 3 was released");
        }
    }

    public void Button4(bool pressed)
    {
        if (pressed)
        {
            print("Button 4 was pressed");
        }
        else
        {
            print("Button 4 was released");
        }
    }

    public void DebugHealthMessage(int dmg, bool remove)
    {
        if (remove)
        {
            server.SendMessage(playerID, 1, dmg);
        }
        else if (!remove)
        {
            server.SendMessage(playerID, 2, dmg);
        }
    }
}
