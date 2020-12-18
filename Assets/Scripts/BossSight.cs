using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Nate Smith
 * Date: 12/4/2020
 * Description: Alerts other code if the player is seen or lost.
 */

public class BossSight : MonoBehaviour
{
    public bool activated = true;
    public bool seesPlayer = false;
    private float timeSeeingPlayer = 0;
    private GameObject lightHolder;
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private float sightRange = 50f;
    [SerializeField] private float sightTimeCountdown = 15f;

    [SerializeField] private LayerMask sightLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        lightHolder = GetComponentInChildren<Light>().gameObject;
        lightHolder.GetComponent<Light>().intensity = 0;
    }

    private void FixedUpdate()
    {
        if (activated || !GameManager.instance.dead)
        {
            Ray bossToPlayer = new Ray(transform.position, (playerManager.transform.position - transform.position).normalized);
            if (Physics.Raycast(bossToPlayer, out RaycastHit hit, sightRange, sightLayers))
            {
                // if the boss has uninterrupted line of sight to player
                if (hit.collider.CompareTag("Player"))
                {
                    timeSeeingPlayer += Time.fixedDeltaTime;

                    lightHolder.transform.LookAt(playerManager.transform);

                    // if the boss couldn't previously see the player
                    if (!seesPlayer)
                    {
                        // trigger boss seeing player
                        CaughtPlayer();
                    }

                    if (timeSeeingPlayer > GameManager.instance.numPillars + sightTimeCountdown)
                    {
                        BossSeenTooLong();
                    }
                }
                else if (seesPlayer)
                {
                    LostPlayer();
                }
            }
            else
            {
                LostPlayer();
            }
        }
        else if (seesPlayer)
        {
            LostPlayer();
        }
    }

    /*
     * Triggers when the boss spots the player after not seeing them.
     */
    private void CaughtPlayer()
    {
        seesPlayer = true;
        Debug.Log("Boss has seen player.");
        lightHolder.GetComponent<Light>().intensity = 100;
        playerManager.BossSawPlayer();
    }

    /*
     * Triggers when the boss loses the player after seeing them.
     */
    private void LostPlayer()
    {
        seesPlayer = false;
        Debug.Log("Boss has lost player.");
        timeSeeingPlayer = 0;
        lightHolder.GetComponent<Light>().intensity = 0;
        playerManager.BossLostPlayer();
    }

    /*
     * Triggers when the boss loses the player after seeing them.
     */
    private void BossSeenTooLong()
    {
        Debug.Log("Had sight of player for too long");
        timeSeeingPlayer = -5;
        playerManager.BossSeenPlayerTooLong(transform.position);
        lightHolder.GetComponent<Light>().intensity = 1000;
    }
}
