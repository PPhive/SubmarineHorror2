using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public static HarpoonGun instance;
    public GameObject LockedTarget;
    public Rigidbody RB;

    public GameObject HarpoonPrefab;
    public GameObject Harpoon;
    public GameObject RopePrefab;
    public GameObject Rope;
    public GameObject LoadedHarpoon;

    public AudioClip FireClip;
    public AudioClip HitClip;
    public AudioClip ReelInClip;
    public AudioClip ReloadClip;

    private AudioSource FireAS;
    private AudioSource HitAS;
    private AudioSource ReelInAS;
    private AudioSource ReloadAS;

    void Awake() 
    {
        instance = this;

        FireAS = gameObject.AddComponent<AudioSource>();
        FireAS.playOnAwake = false;
        FireAS.clip = FireClip;

        HitAS = gameObject.AddComponent<AudioSource>();
        HitAS.playOnAwake = false;
        HitAS.clip = HitClip;

        ReelInAS = gameObject.AddComponent<AudioSource>();
        ReelInAS.playOnAwake = false;
        ReelInAS.clip = ReelInClip;

        ReloadAS = gameObject.AddComponent<AudioSource>();
        ReloadAS.playOnAwake = false;
        ReloadAS.clip = ReelInClip;
    }

    void Start()
    {
        LoadedHarpoon.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadedHarpoon.SetActive(false);
            FireSound();
            if (Harpoon == null)
            {
                Instantiate(HarpoonPrefab, transform.position, transform.rotation);
            }
            if (Rope == null) 
            {
                Instantiate(RopePrefab, transform.position, transform.rotation);
            }
        }
        if (Input.GetMouseButtonUp(0) && true) 
        {
            Destroy(Harpoon);
            Destroy(Rope);
            LoadedHarpoon.SetActive(true);
            ReloadSound();
        }
    }

    public void FireSound()
    {
        if (FireClip)
            return;
        FireAS.Play();
    }

    public void HitSound()
    {
        if (HitClip)
            return;
        HitAS.Play();
    }

    public void ReelInSound()
    {
        if (ReelInClip)
            return;
        ReelInAS.Play();
    }

    public void StopReelInSound()
    {
        if (!ReelInClip)
            return;
            if (ReelInAS.isPlaying)
            ReelInAS.Stop();
    }

    public void ReloadSound()
    {
        if (ReloadClip)
            return;
        ReloadAS.Play();
    }
}
