using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamineExpl : MonoBehaviour {

    private string fromWhichPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(fromWhichPlayer, 2);
        }
        Destroy(gameObject);
    }

    public void SetAttacker(string playerName)
    {
        fromWhichPlayer = playerName;
    }
}
