using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour {

    public Transform mine;
    public Transform shell;

    public Color mineBoxColor;
    public Color shellBoxColor;

    public string which;

    private void Start()
    {
        if (which == "mine")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = mineBoxColor;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = shellBoxColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (which == "mine")
            {
                other.GetComponent<PlayerShooting>().minesCount++;
            }
            else
            {
                other.GetComponent<PlayerShooting>().shellCount++;
            }            
            Destroy(gameObject);
        }
    }
}
