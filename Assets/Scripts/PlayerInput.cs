using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput: PlayerStats
{

    //public int playerID;
    

    //public float speed = 10.0f;
    //public float rotationSpeed = 100.0f;
    
    //public float axisH;
    //public float axisV;
    
    public Transform fireRight;
    public Transform fireLeft;
    private string shootMode = "short";


    void Update ()
    {

        //float translation = CrossPlatformInputManager.GetAxis(axisNameV) * speed * Time.deltaTime;
        //transform.Translate(0, 0, translation);

        //float rotation = CrossPlatformInputManager.GetAxis(axisNameH) * rotationSpeed;

        float rotation = axisH * rotationSpeed;
        rotation *= Time.deltaTime;

        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.Rotate(0, rotation, 0);
    }

    // shoot left
    public void ShootLeft(bool pressed)
    {
        if (pressed)
        {
            print("Button 1 was pressed");

            if (shootMode == "short")
            {
               GetComponent<PlayerShooting>().ShortRangeShoot(fireLeft);
            }

            else if (shootMode == "long")
            {
                GetComponent<PlayerShooting>().LongRangeShoot(fireLeft);
            }
            
            else if (shootMode == "seamine")
            {
                GetComponent<PlayerShooting>().Seamine();
            }
        }
        else
        {
            print("Button 1 was released");
        }
    }

    // shoot right
    public void ShootRight(bool pressed)
    {
       
        if (pressed)
        {
            print("Button 2 was pressed");

            if (shootMode == "short")
            {
                
                GetComponent<PlayerShooting>().ShortRangeShoot(fireRight);
            }
            
            else if (shootMode == "long")
            {
                GetComponent<PlayerShooting>().LongRangeShoot(fireRight);
            }

            else if (shootMode == "seamine")
            {
                GetComponent<PlayerShooting>().Seamine();
            }
        }
        else
        {
            print("Button 2 was released");
        }
    }

    // swap weapons
    public void SwapWeapons(bool pressed)
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

}
