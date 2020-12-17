using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOREtex : MonoBehaviour
{

    private Rigidbody playerRB;

    private bool playerInside = false;

    [SerializeField] private float rotationPower = 500f;
    [SerializeField] private float upwardForce = 2000f;

    private void Start()
    {
        playerRB = PlayerManager.instance.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerInside)
        {
            Vector3 force = transform.up * upwardForce;
            playerRB.AddForce(force, ForceMode.Force);

            Vector3 rot = new Vector3(0f, rotationPower, 0f);
            playerRB.AddTorque(rot, ForceMode.Force);
        }
    }

}
