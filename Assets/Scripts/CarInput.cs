using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarInput: MonoBehaviour {

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public string axisNameV;
    public string axisNameH;

    void Update () {
        
            float translation = CrossPlatformInputManager.GetAxis(axisNameV) * speed;
            float rotation = CrossPlatformInputManager.GetAxis(axisNameH) * rotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);
 
	}

    public void Button1(bool pressed)
    {
        if (pressed)
        {
            print("Button 1 was pressed");
        }
        else
        {
            print("Button 1 was released");
        }
    }

    public void Button2(bool pressed)
    {
        if (pressed)
        {
            print("Button 2 was pressed");
        }
        else
        {
            print("Button 2 was released");
        }
    }

    public void Button3(bool pressed)
    {
        if (pressed)
        {
            print("Button 3 was pressed");
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
