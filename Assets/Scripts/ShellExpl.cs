using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExpl : MonoBehaviour
{

    public float maxLifetime;
    private string fromWhichPlayer;
    GameObject attackingPlayer;

	void Start ()
    {
        Destroy(gameObject, maxLifetime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(attackingPlayer, 1);
            Destroy(gameObject);
        }
    }

    public void SetShooter (GameObject shooter)
    {
        attackingPlayer = shooter;
    }

    public void SetMaxLifetime (float sec)
    {
        maxLifetime = sec;
    }
}
