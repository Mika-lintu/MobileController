using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject cooldownObject;
    public float cooldownTime = 2f;
    bool cooldownActive = false;
    Slider cooldownSlider;

    // Use this for initialization
    void Start () {
        cooldownSlider = cooldownObject.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldownActive)
        {
            if (cooldownSlider.value <= cooldownTime)
            {
                cooldownSlider.value += Time.deltaTime;
            }
            else
            {
                cooldownActive = false;
            }
        }
	}

    public void activateCooldown()
    {
        cooldownSlider.value = 0;
        cooldownActive = true;
    }
}
