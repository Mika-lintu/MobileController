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

    public void Button1()
    {
        print("Button 1 was pressed");
    }

    public void Button2()
    {
        print("Button 2 was pressed");
    }

    public void Button3()
    {
        print("Button 3 was pressed");
    }

    public void Button4()
    {
        print("Button 4 was pressed");
    }

}
