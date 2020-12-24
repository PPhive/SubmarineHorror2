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
 * rocks falling, lava capsules scale on y axis increasing, once they reach ground, have plane start to move upwards, if touch lava, die
 */

public class EndSequenceManager : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private Text timeToLeaveText = null;

    [Header("Scene Elements")]
    // BoulderSpawner prefabs must be placed around the scene.
    [SerializeField] private BoulderSpawner[] boulderSpawners = null;

    [SerializeField] private EndingTrigger endingTrigger = null;

    [SerializeField] private Transform[] lavaSpires = null;
    [SerializeField] private LavaManager lavaLake = null;

    [SerializeField] private Light[] directionalLights = null;
    [SerializeField] private ParticleSystem[] particleSystems = null;

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
        boulderSpawners = FindObjectsOfType<BoulderSpawner>();
        foreach (BoulderSpawner boulderSpawner in boulderSpawners)
        {
            boulderSpawner.gameObject.SetActive(false);
        }

        timeToLeaveText.enabled = false;

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

        lavaLake = FindObjectOfType<LavaManager>();
        lavaLake.gameObject.SetActive(false);

        directionalLights = GetComponentsInChildren<Light>();
        foreach (Light light in directionalLights)
        {
            light.enabled = false;
        }

        particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartEndSequence();
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

        StartCoroutine(LavaSequenceEnum());

        foreach (ParticleSystem ps in particleSystems)
        {
            ps.gameObject.SetActive(true);
        }
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

    private IEnumerator LavaSequenceEnum()
    {
        foreach (Light light in directionalLights)
        {
            light.enabled = true;
            light.intensity = 0f;
        }
        float duration = 10f;
        float elapsedTime = 0f;
        Vector3 initScale = new Vector3(5f, 0f, 5f);
        Vector3 finalScale = new Vector3(5f, 45f, 5f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float fraction = elapsedTime / duration;

            foreach (Transform trans in lavaSpires)
            {
                trans.localScale = Vector3.Lerp(initScale, finalScale, fraction);
            }
            foreach (Light light in directionalLights)
            {
                light.intensity = Mathf.Lerp(0f, .2f, fraction);
            }

            yield return null;
        }


        lavaLake.gameObject.SetActive(true);
        
    }


    private IEnumerator EndSequenceEnum()
    {
        // make rocks fall from ceiling, get lava columns to descend, get lava plane to raise, first start out slow then go faster

        PlayerManager.instance.camFX.ambientRumbling = true;
        rumblingAS.Play();
        rockCrashAS.Play();
        rushingWaterAS.Play();



        yield return new WaitForSeconds(endTimeLimit);

        // after endTimeLimit seconds, end sequence

        EndEndSequence();
    }

    public void EndEndSequence()
    {
        GameManager.instance.WinSequence();
    }
}
