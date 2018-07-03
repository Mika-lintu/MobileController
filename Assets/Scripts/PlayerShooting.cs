using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public int minesCount = 3;
    public int shellCount = 3;

    public Transform seamine;
    private float mineOffset = 2.9f;
    public Rigidbody shell;
    public float shellVelocity = 30f;

    private float lastShotShort = -3;
    private float shortCooldown = 3f;
    private float lastShotLong = -1;
    private float longCooldown = 1f;
    private float lastSeamine = -1.5f;
    private float seamineCooldown = 1.5f;

    PlayerInput playerInput;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    // shoots from all cannons
    public void ShortRangeShoot (Transform side)
    {
        if (Time.time >= lastShotShort + shortCooldown && shellCount > 0)
        {
            shellCount--;
            playerInput.DebugAmmoMessage(1, true);
            foreach (Transform child in side.transform)
            {
                Shoot(child, 0.5f);
            }
            lastShotShort = Time.time;
        }
    }

    // shoots from middle cannon only
    public void LongRangeShoot (Transform side)
    {   
        if (Time.time >= lastShotLong + longCooldown)
        {
            Shoot(side.transform.GetChild(1), 3f);
            lastShotLong = Time.time;
        }      
    }

    public void Seamine ()
    {
        if (Time.time >= lastSeamine + seamineCooldown && minesCount > 0)
        {
            minesCount--;
            playerInput.DebugMineMessage(1, true);
            Transform mine = Instantiate(seamine, transform.position - transform.forward * mineOffset, transform.rotation);
            mine.GetComponent<SeamineExpl>().SetAttacker(name);
            lastSeamine = Time.time;
        }       
    }


    private void Shoot (Transform cannon, float lifetime)
    {
        // set lifetime and launch shell
        Rigidbody shellInstance = Instantiate(shell, cannon.position, cannon.rotation) as Rigidbody;
        ShellExpl shellScript = shellInstance.GetComponent<ShellExpl>();
        shellScript.SetMaxLifetime(lifetime);
        shellScript.enabled = true;
        shellInstance.AddForce(cannon.forward * shellVelocity, ForceMode.VelocityChange);

        // keep track of the player who's shooting (for points)
        shellInstance.GetComponent<ShellExpl>().SetShooter(gameObject.name);

        // play effects
        Transform explObject = cannon.GetChild(0);
        Transform smokeObject = cannon.GetChild(1);

        ParticleSystem expl = explObject.GetComponent<ParticleSystem>();
        ParticleSystem smoke = smokeObject.GetComponent<ParticleSystem>();

        expl.Play();
        smoke.Play();
    }

    public void AddShell()
    {
        shellCount++;
        playerInput.DebugAmmoMessage(1, false);
    }

    public void AddMine()
    {
        minesCount++;
        playerInput.DebugMineMessage(1, false);
    }
}
