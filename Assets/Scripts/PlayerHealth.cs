using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float deadSeconds = 1f;
    private int maxHealth = 3;
    private int health;

    public Transform spawnPoint;

    private SpriteRenderer spriteRenderer;
    public Sprite fullHP;
    public Sprite twoHP;
    public Sprite oneHP;
    public Sprite noHP;

    private void Start()
    {
        // get the ship's sprite renderer and set health
        spriteRenderer = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // decrease health and change sprite based on health
    public void TakeDamage ()
    {
        Debug.Log("Damage taken.");
        health--;

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
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        spriteRenderer.sprite = noHP;
        
        // disable shooting & movement
        gameObject.GetComponent<CarInput>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(deadSeconds);

        // respawn, enable shooting & movement
        gameObject.SetActive(false);
        spriteRenderer.sprite = fullHP;
        gameObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        gameObject.SetActive(true);

        health = maxHealth;
        gameObject.GetComponent<CarInput>().enabled = true;
    }
}
