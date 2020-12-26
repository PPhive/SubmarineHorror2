using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    private Coroutine stoppingMoveSoundCO = null;

    private AudioSource impactAS;
    private AudioSource ambientBossRumbleAS;
    private AudioSource bossCaughtPlayerAS;
    private AudioSource bossAttackPlayerAS;
    private AudioSource subMovementAS;
    private AudioSource ambientWaterAS1;
    private AudioSource ambientWaterAS2;

    [SerializeField] private AudioClip[] impactClips = null; // 0 = hi, 1 = med, 2 = low
    [SerializeField] private AudioClip ambientBossRumbleClip = null;
    [Range(0.0f, 1.0f), SerializeField] private float ambientBossRumbleVolume = 1f;
    [SerializeField] private AudioClip bossCaughtPlayerClip = null;
    [Range(0.0f, 1.0f), SerializeField] private float bossCaughtPlayerVolume = 1f;
    [SerializeField] private AudioClip bossAttackPlayerClip = null;
    [Range(0.0f, 1.0f), SerializeField] private float bossAttackPlayerVolume = 1f;
    [SerializeField] private AudioClip subMovementClip = null;
    [Range(0.0f, 1.0f), SerializeField] private float subMovementVolume = .5f;
    [SerializeField] private AudioClip[] ambientWaterClips = null;
    [Range(0.0f, 1.0f), SerializeField] private float ambientWaterVolume = .4f;

    private void Awake()
    {
        impactAS = gameObject.AddComponent<AudioSource>();
        impactAS.playOnAwake = false;

        ambientBossRumbleAS = gameObject.AddComponent<AudioSource>();
        ambientBossRumbleAS.playOnAwake = false;
        ambientBossRumbleAS.loop = true;
        ambientBossRumbleAS.volume = ambientBossRumbleVolume;
        ambientBossRumbleAS.clip = ambientBossRumbleClip;

        bossCaughtPlayerAS = gameObject.AddComponent<AudioSource>();
        bossCaughtPlayerAS.playOnAwake = false;
        bossCaughtPlayerAS.loop = false;
        bossCaughtPlayerAS.volume = bossCaughtPlayerVolume;
        bossCaughtPlayerAS.clip = bossCaughtPlayerClip;

        bossAttackPlayerAS = gameObject.AddComponent<AudioSource>();
        bossAttackPlayerAS.playOnAwake = false;
        bossAttackPlayerAS.loop = false;
        bossAttackPlayerAS.volume = bossAttackPlayerVolume;
        bossAttackPlayerAS.clip = bossAttackPlayerClip;

        subMovementAS = gameObject.AddComponent<AudioSource>();
        subMovementAS.playOnAwake = false;
        subMovementAS.loop = true;
        subMovementAS.volume = subMovementVolume;
        subMovementAS.clip = subMovementClip;

        ambientWaterAS1 = gameObject.AddComponent<AudioSource>();
        ambientWaterAS1.volume = ambientWaterVolume;
        ambientWaterAS1.playOnAwake = false;
        ambientWaterAS2 = gameObject.AddComponent<AudioSource>();
        ambientWaterAS2.volume = ambientWaterVolume;
        ambientWaterAS2.playOnAwake = false;
        StartCoroutine(AmbientWaterEnum());
    }

    private IEnumerator AmbientWaterEnum()
    {
        while (true)
        {
            AudioClip curClip = ambientWaterClips[Random.Range(0, ambientWaterClips.Length)];
            ambientWaterAS1.clip = curClip;
            ambientWaterAS1.Play();

            yield return new WaitForSeconds(curClip.length - 2f);

            curClip = ambientWaterClips[Random.Range(0, ambientWaterClips.Length)];
            ambientWaterAS2.clip = curClip;
            ambientWaterAS2.Play();

            yield return new WaitForSeconds(curClip.length - 2f);
        }
    }

    public void Moving()
    {
        // if the stop move sound CO is being used, turn it off.
        if (stoppingMoveSoundCO != null)
        {
            StopCoroutine(stoppingMoveSoundCO);
            stoppingMoveSoundCO = null;
        }

        subMovementAS.volume = subMovementVolume;
        if (!subMovementAS.isPlaying || subMovementAS.pitch == 1.1f)
        {
            subMovementAS.pitch = 1f;
            float time = subMovementAS.time;
            subMovementAS.Play();
            subMovementAS.time = time;
        }
    }

    public void MovingFast()
    {
        // if the stop move sound CO is being used, turn it off.
        if (stoppingMoveSoundCO != null)
        {
            StopCoroutine(stoppingMoveSoundCO);
            stoppingMoveSoundCO = null;
        }

        subMovementAS.volume = subMovementVolume;
        if (!subMovementAS.isPlaying || subMovementAS.pitch == 1f)
        {
            // doesn't correctly change pitch
            subMovementAS.pitch = 1.1f;
            float time = subMovementAS.time;
            subMovementAS.Play();
            subMovementAS.time = time;
        }
    }

    public void NotMoving()
    {
        if (subMovementAS.isPlaying && stoppingMoveSoundCO == null)
        {
            //subMovementAS.Stop();
            // lerp to a stop!!!!!!!!!!!!!!!
            stoppingMoveSoundCO = StartCoroutine(NotMovingEnum());
        }
    }

    private IEnumerator NotMovingEnum()
    {
        // Lerp volume to 0 quickly
        float elapsedTime = 0f;
        float duration = .5f;
        float startVol = subMovementAS.volume;
        float targetVol = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            subMovementAS.volume = Mathf.Lerp(startVol, targetVol, elapsedTime / duration);
            yield return null;
        }

        subMovementAS.volume = targetVol;
        stoppingMoveSoundCO = null;
        subMovementAS.Stop();
    }

    public void HeavyImpact()
    {
        impactAS.clip = impactClips[0];
        impactAS.pitch = Random.Range(.9f, 1.1f);
        impactAS.Play();
    }

    public void MedImpact()
    {

        impactAS.clip = impactClips[1];
        impactAS.pitch = Random.Range(.9f, 1.1f);
        impactAS.Play();
    }

    public void LowImpact()
    {
        impactAS.clip = impactClips[2];
        impactAS.pitch = Random.Range(.9f, 1.1f);
        impactAS.Play();
    }

    public void ActivateAmbientBossRumble()
    {
        ambientBossRumbleAS.Play();
    }

    public void DeactivateAmbientBossRumble()
    {
        if (!GameManager.instance.endSequence)
            ambientBossRumbleAS.Stop();
    }

    public void BossCaughtPlayer()
    {
        bossCaughtPlayerAS.Play();
    }

    public void BossAttackPlayer()
    {
        bossAttackPlayerAS.Play();
    }

    public void DeathSounds()
    {

    }

    
}
