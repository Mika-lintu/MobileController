using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public Rigidbody shell;
    public float shellVelocity = 30f;


    public void ShortRangeShoot (Transform side)
    {
        // shoots from all cannons
        foreach (Transform child in side.transform)
        {
            Shoot(child);
        }
    }

	public void LongRangeShoot (Transform side)
    {   
        // shoots from middle cannon only
        Shoot(side.transform.GetChild(1));
    }

    private void Shoot (Transform cannon)
    {
        Rigidbody shellInstance = Instantiate(shell, cannon.position, cannon.rotation) as Rigidbody;
        shellInstance.AddForce(cannon.forward * shellVelocity, ForceMode.VelocityChange);

        Transform explObject = cannon.GetChild(0);
        Transform smokeObject = cannon.GetChild(1);

        ParticleSystem expl = explObject.GetComponent<ParticleSystem>();
        ParticleSystem smoke = smokeObject.GetComponent<ParticleSystem>();

        expl.Play();
        smoke.Play();
    }
}
