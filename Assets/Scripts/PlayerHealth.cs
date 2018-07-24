using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private float deadSeconds = 1f;
    private int maxHealth = 3;
    private int health;

    public Transform spawnPoint;

    private SpriteRenderer spriteRenderer;
    public Sprite fullHP;
    public Sprite twoHP;
    public Sprite oneHP;
    public Sprite noHP;

    GameManager manager;
    PlayerInput input;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        input = GetComponent<PlayerInput>();
        // get the ship's sprite renderer and set health
        spriteRenderer = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // decrease health and change sprite based on health (name: attacker)
    public void TakeDamage (GameObject attackingPlayer, int damage)
    {
        health -= damage;
        input.HealthMessage(damage, true);
        //input.DebugHealthMessage(damage, true);

        if (health == 2)
        {
            spriteRenderer.sprite = twoHP;
        }
        else if (health == 1)
        {
            spriteRenderer.sprite = oneHP;
        }
        else if (health <= 0)
        {
            StartCoroutine(PlayerDeath(attackingPlayer));
        }
    }

    private IEnumerator PlayerDeath(GameObject shooterPlayer)
    {
        spriteRenderer.sprite = noHP;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        int shooterID = shooterPlayer.GetComponent<PlayerInput>().playerID;
        // add a point to the player who destroyed ship
        if (input.playerID != shooterID)
        {
           manager.AddPoint(shooterPlayer);
        }       

        // disable shooting & movement
        input.enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(deadSeconds);

        // respawn, enable shooting, taking damage & movement
        gameObject.SetActive(false);
        spriteRenderer.sprite = fullHP;
        gameObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        gameObject.SetActive(true);

        health = maxHealth;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        input.HealthMessage(3, false);
        input.enabled = true;

        
        //input.DebugHealthMessage(maxHealth, false);
    }
}
