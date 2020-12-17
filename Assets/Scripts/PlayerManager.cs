using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Main manager for player. Contains references to movement.
 */

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance = null;

    public int health = 5;
    [SerializeField] private Light flashlightDim = null;
    [SerializeField] private Light flashlightBright = null;

    private float lastCollisionTime = 0f;
    [SerializeField] private float collisionCooldown = 2f;

    private SubmarineMovement movement;
    public CameraFX camFX;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] collisionSounds; // 0 = hi, 1 = med, 2 = low

    private void Awake()
    {
        // Ensure that there is only one instance of the PlayerManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        movement = GetComponent<SubmarineMovement>();
        camFX = GetComponentInChildren<CameraFX>();

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
    }

    // handle damage from fast collisions
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 5f)
        {
            camFX.HighShake();
            audioSource.clip = collisionSounds[0];
            audioSource.pitch = Random.Range(.9f, 1.1f);
            audioSource.Play();
            // Major hit
            if (lastCollisionTime + collisionCooldown < Time.time)
            {
                lastCollisionTime = Time.time;
                TakeDamage(1);
                // damage animation
                // collision sound
            }
        }
        else if (collision.relativeVelocity.magnitude > 3f)
        {
            // Minor hit
            camFX.MedShake();
            audioSource.clip = collisionSounds[1];
            audioSource.pitch = Random.Range(.9f, 1.1f);
            audioSource.Play();
        }
        else if (collision.relativeVelocity.magnitude > 1f)
        {
            // Very minor hit
            camFX.LowShake();
            audioSource.clip = collisionSounds[2];
            audioSource.pitch = Random.Range(.9f, 1.1f);
            audioSource.Play();
        }
    }

    public void BossSeenTooLong()
    {
        TakeDamage(1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (!flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightDim.enabled = true;
            }
            else if (flashlightDim.enabled && !flashlightBright.enabled)
            {
                flashlightBright.enabled = true;
                flashlightDim.enabled = false;
            }
            else
            {
                flashlightBright.enabled = false;
                flashlightDim.enabled = false;
            }
        }
    }
}
