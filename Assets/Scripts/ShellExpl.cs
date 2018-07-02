using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExpl : MonoBehaviour {

    public float maxLifetime;
    private string fromWhichPlayer;

	void Start () {
        Destroy(gameObject, maxLifetime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(fromWhichPlayer, 1);
            Destroy(gameObject);
        }
    }

    public void SetShooter (string name)
    {
        fromWhichPlayer = name;
    }

    public void SetMaxLifetime (float sec)
    {
        maxLifetime = sec;
    }
}
