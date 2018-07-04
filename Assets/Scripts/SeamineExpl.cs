using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamineExpl : MonoBehaviour {

    private string fromWhichPlayer;
    GameObject attackingPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage( attackingPlayer, 2);
        }
        Destroy(gameObject);
    }

    public void SetAttacker(GameObject attacker)
    {
        attackingPlayer = attacker;
    }
}
