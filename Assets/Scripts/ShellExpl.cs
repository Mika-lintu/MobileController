using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExpl : MonoBehaviour {

    public float maxLifetime = 3f;

	void Start () {
        Destroy(gameObject, maxLifetime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage();
        }     
        Destroy(gameObject);
    }
}
