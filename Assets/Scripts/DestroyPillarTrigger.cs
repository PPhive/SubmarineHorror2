using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyPillarTrigger : MonoBehaviour
{

    private bool playerInArea = false;

    [SerializeField] private Text bombPlaceText = null;

    private void Start()
    {
        bombPlaceText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
            bombPlaceText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
            bombPlaceText.enabled = false;
        }
    }

    private void Update()
    {
        if (playerInArea && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInParent<DestroyPillar>().TriggerExplosionCountdown();
            gameObject.SetActive(false);
            bombPlaceText.enabled = false;
        }
    }
}
