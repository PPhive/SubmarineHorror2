using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPillarTrigger : MonoBehaviour
{

    private bool playerInArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    private void Update()
    {
        if (playerInArea && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInParent<DestroyPillar>().TriggerExplosionCountdown();
            gameObject.SetActive(false);
        }
    }
}
