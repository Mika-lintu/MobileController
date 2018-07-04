using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool cooldownActive = false;

    public Slider longShotCooldown;
    public Slider shortShotCooldown;
    public Slider mineCooldown;
    public Slider health;

    public Text weaponText;
    public Text ammoText;
    public Text mineText;
    public Text pointsText;

    int ammoAmount = 3;
    int mineAmount = 3;
    int pointsAmount = 0;

    enum cooldownSliderType { LongShot, ShortShot, Mine}
    cooldownSliderType  currentCooldown;

    void Start ()
    {
        currentCooldown = cooldownSliderType.ShortShot;
        weaponText.text = "Active Weapon: Short range";
        ammoText.text = "Ammo: " + ammoAmount;
        mineText.text = "Mines: " + mineAmount;
        pointsText.text = "Points: " + pointsAmount;
    }
	
	
	void Update ()
    {
        
    }

    public void ActivateCooldown(int buttonID)
    {
        if (!cooldownActive)
        {
            if (buttonID == 1 || buttonID == 2)
            {

                if (currentCooldown == cooldownSliderType.ShortShot)
                {
                    shortShotCooldown.value = 0;
                    StartCoroutine(Cooldown(3f, shortShotCooldown));
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
    }

    public void SwapWeapons(int buttonID)
    {
        if (currentCooldown == cooldownSliderType.ShortShot)
        {
            currentCooldown = cooldownSliderType.LongShot;
            weaponText.text = "Active Weapon: Long range";
        }
        else if (currentCooldown == cooldownSliderType.LongShot)
        {
            currentCooldown = cooldownSliderType.Mine;
            weaponText.text = "Active Weapon: Mines";
        }
        else
        {
            currentCooldown = cooldownSliderType.ShortShot;
            weaponText.text = "Active Weapon: Short range";
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

    public void ResetUI(int dmg)
    {
        ammoAmount = 3;
        mineAmount = 3;
        health.value = health.maxValue;
        ammoText.text = "Ammo: " + ammoAmount;
        mineText.text = "Mines: " + mineAmount;
    }

    public void RemoveHealth(int dmg)
    {
        health.value -= dmg;
    }

    public void RemoveAmmo(int amount)
    {
        ammoAmount -= amount;
        ammoText.text = "Ammo: " + ammoAmount;
    }

    public void AddAmmo(int amount)
    {
        ammoAmount += amount;
        ammoText.text = "Ammo: " + ammoAmount ;
    }

    public void RemoveMine(int amount)
    {
        mineAmount -= amount;
        mineText.text = "Mines: " + mineAmount;
    }

    public void AddMine(int amount)
    {
        mineAmount += amount;
        mineText.text = "Mines: " + mineAmount;
    }
    
}
