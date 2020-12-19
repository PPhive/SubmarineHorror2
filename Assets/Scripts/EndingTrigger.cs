using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{

    private EndSequenceManager endSequenceManager;

    private void Start()
    {
        endSequenceManager = FindObjectOfType<EndSequenceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endSequenceManager.EndEndSequence();
        }
    }
}
