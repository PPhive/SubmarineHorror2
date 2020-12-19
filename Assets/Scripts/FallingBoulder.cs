using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoulder : MonoBehaviour
{
    [SerializeField] private AudioClip[] impacts = null;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 10f)
        {
            audioSource.clip = impacts[Random.Range(0, 1)];
            audioSource.pitch = Random.Range(.85f, 1.1f);
            audioSource.Play();
        }
    }
}
