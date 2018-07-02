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
	
	
	void Update () {
        
    }

    public void ActivateCooldown(int buttonID)
    {
       
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
        }
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
        cooldownActive = true;
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
