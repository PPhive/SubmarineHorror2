using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 
 * Enable the BoulderSpawners, - done
 * Start huge rumbling
 * spawn rocks around scene (away from player?)
 * enableEndTrigger - done
 * start countdown - done
 * if player doesn't get out by countdown, make big crash sound then fade to black, say need to get out in time.
 * rushingwater sound as you exit, fade other sounds away
 */

public class EndSequenceManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private Text timeToLeaveText = null;
    [SerializeField] private Text timeLeftText = null;
    [SerializeField] private Text tooSlowDeathText = null;

    [Header("Scene Elements")]
    // BoulderSpawner prefabs must be placed around the scene.
    [SerializeField] private BoulderSpawner[] boulderSpawners = null;

    [SerializeField] private EndingTrigger endingTrigger = null;

    [Header("Time")]
    [SerializeField] private float endTimeLimit = 360f; // 6 mins, probs not necessary;
    private float countDown = 10f;

    [Header("Audio")]
    private AudioSource rumblingAS;
    [SerializeField] private AudioClip rumblingClip = null;
    private AudioSource rockCrashAS;
    [SerializeField] private AudioClip rockCrashClip = null;
    private AudioSource rushingWaterAS;
    [SerializeField] private AudioClip rushingWaterClip = null;



    private void Start()
    {
        foreach (BoulderSpawner boulderSpawner in boulderSpawners)
        {
            boulderSpawner.gameObject.SetActive(false);
        }

        timeToLeaveText.enabled = false;
        timeLeftText.enabled = false;

        endingTrigger = GetComponentInChildren<EndingTrigger>();
        endingTrigger.gameObject.SetActive(false);
        countDown = endTimeLimit;

        rumblingAS = gameObject.AddComponent<AudioSource>();
        rumblingAS.playOnAwake = false;
        rumblingAS.loop = true;
        rumblingAS.clip = rumblingClip;

        rockCrashAS = gameObject.AddComponent<AudioSource>();
        rockCrashAS.playOnAwake = false;
        rockCrashAS.loop = true;
        rockCrashAS.clip = rockCrashClip;

        rushingWaterAS = gameObject.AddComponent<AudioSource>();
        rushingWaterAS.playOnAwake = false;
        rushingWaterAS.loop = true;
        rushingWaterAS.clip = rushingWaterClip;

        tooSlowDeathText.enabled = false;
    }

    public void StartEndSequence()
    {
        // enable each boulder
        foreach (BoulderSpawner boulderSpawner in boulderSpawners)
        {
            boulderSpawner.gameObject.SetActive(true);
        }

        endingTrigger.gameObject.SetActive(true);

        StartCoroutine(TimeToLeaveTextFadeEnum());
        StartCoroutine(EndSequenceEnum());
    }

    private IEnumerator TimeToLeaveTextFadeEnum()
    {
        timeToLeaveText.enabled = true;

        timeToLeaveText.color = new Color(1f, 1f, 1f, 0f);
        float duration = 1f;
        float elapsedTime = 0f;
        Color initColor = new Color(1f, 1f, 1f, 0f);
        Color finalColor = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            timeToLeaveText.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);
            yield return null;
        }
        timeToLeaveText.color = finalColor;

        yield return new WaitForSeconds(5f);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            timeToLeaveText.color = Color.Lerp(finalColor, initColor, elapsedTime / duration);
            yield return null;
        }
        timeToLeaveText.color = initColor;
        timeToLeaveText.enabled = false;
    }

    private IEnumerator EndSequenceEnum()
    {
        // make rocks fall from ceiling, get lava columns to descend, get lava plane to raise, first start out slow then go faster

        PlayerManager.instance.camFX.ambientRumbling = true;


        timeLeftText.enabled = true;
        timeLeftText.text = countDown.ToString();

        timeLeftText.color = new Color(1f, 1f, 1f, 0f);
        float duration = 1f;
        float elapsedTime = 0f;
        Color initColor = new Color(1f, 1f, 1f, 0f);
        Color finalColor = new Color(1f, 1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            timeLeftText.color = Color.Lerp(initColor, finalColor, elapsedTime / duration);
            yield return null;
        }
        timeLeftText.color = finalColor;

        while (countDown >= 0)
        {
            if (countDown == 60)
            {
                timeLeftText.color = Color.red;
            }
            countDown--;
            timeLeftText.text = countDown.ToString();
            yield return new WaitForSeconds(1f);
        }

        // after endTimeLimit seconds, end sequence

        EndEndSequence();
    }

    public void EndEndSequence()
    {
        GameManager.instance.WinSequence();
    }
}
