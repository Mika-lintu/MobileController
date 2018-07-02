using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

   

    public bool cooldownActive = false;
    public Slider longShotCooldown;
    public Slider shortShotCooldown;
    public Slider mineCooldown;
    public Slider health;
    public Text weaponText;

  
    enum cooldownSliderType { LongShot, ShortShot, Mine}
    cooldownSliderType  currentCooldown;

    void Start ()
    {
        currentCooldown = cooldownSliderType.ShortShot;
        weaponText.text = "Short range";
    }
	
	// Update is called once per frame
	void Update () {

      //  if (cooldownActive)
      //  {
      //      if (currentCooldown == cooldownSliderType.LongShot)
      //      {
      //          cooldownActive = DoCoolDown(3f,longShotCooldown);
      //      }
      //      else if (currentCooldown == cooldownSliderType.ShortShot)
      //      {
      //          cooldownActive = DoCoolDown(3f, shortShotCooldown);
      //      }
      //      else if (currentCooldown == cooldownSliderType.Mine)
      //      {
      //          cooldownActive = DoCoolDown(2f, mineCooldown);
      //      }
      //  }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            RemoveHealth(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ActivateCooldown(2);
        }

    }

    public void ActivateCooldown(int buttonID)
    {
       // if(buttonID == 3)
       // {
       //     if (currentCooldown == cooldownSliderType.ShortShot)
       //     {
       //         currentCooldown = cooldownSliderType.LongShot;
       //     }
       //     else if (currentCooldown == cooldownSliderType.LongShot)
       //     {
       //         currentCooldown = cooldownSliderType.Mine;
       //     }
       //     else
       //     {
       //         currentCooldown = cooldownSliderType.ShortShot;
       //     }
       // }
        if (buttonID == 1 || buttonID == 2)
        {
            
            if (currentCooldown == cooldownSliderType.ShortShot)
            {
                shortShotCooldown.value = 0;
                StartCoroutine(Cooldown(3f,shortShotCooldown));
            }
            else if (currentCooldown == cooldownSliderType.LongShot)
            {
                longShotCooldown.value = 0;
                StartCoroutine(Cooldown(3f, longShotCooldown));
            }
            else if (currentCooldown == cooldownSliderType.Mine)
            {
                mineCooldown.value = 0;
                StartCoroutine(Cooldown(3f, mineCooldown));
            }
            //StopAllCoroutines();
        }
                //print("ACTIVATED");
                //if (buttonID == 1)
                //{
                //    longShotCooldown.value = 0;
                //    currentCooldown = cooldownSliderType.LongShot;
                //    print("LongShot");
                //    cooldownActive = true;
                //}
                //else if (buttonID == 2)
                //{
                //    shortShotCooldown.value = 0;
                //    currentCooldown = cooldownSliderType.ShortShot;
                //    print("ShortShot");
                //    cooldownActive = true;
                //}
                //else if (buttonID == 3)
                //{
                //    cooldownActive = false;  
                //    print("Minedrop");
                //}
                //cooldownActive = true;
    }

    public void SwapWeapons(int buttonID)
    {
        if (currentCooldown == cooldownSliderType.ShortShot)
        {
            currentCooldown = cooldownSliderType.LongShot;
            weaponText.text = "Long range";
        }
        else if (currentCooldown == cooldownSliderType.LongShot)
        {
            currentCooldown = cooldownSliderType.Mine;
            weaponText.text = "Mines";
        }
        else
        {
            currentCooldown = cooldownSliderType.ShortShot;
            weaponText.text = "Short range";
        }
    }

    bool DoCoolDown(float cooldownTime, Slider coolDown)
    {
        if (coolDown.value != cooldownTime)
        {
            coolDown.value += Time.deltaTime;
            return true;
        }
        else
        {
            print("DONE");
            return false;
        }
        
    }

    IEnumerator Cooldown(float cooldownTime, Slider coolDown)
    {
        //while (coolDown.value <= cooldownTime)
        //{
        //    coolDown.value += Time.deltaTime;
        //     yield return null;
        //}
        cooldownActive = true;
        float duration = 3f; // 3 seconds you can change this 
                             //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= cooldownTime)
        {
            coolDown.value = normalizedTime;
            normalizedTime += Time.deltaTime;
            yield return null;
        }
        Test();
    }

    void Test()
    {
        print("HEP");
        StopAllCoroutines();
        cooldownActive = false;
    }

    public void RemoveHealth(int dmg)
    {
        health.value -= dmg;
    }

    public void ResetUI(int dmg)
    {
        health.value = health.maxValue;
    }
}
