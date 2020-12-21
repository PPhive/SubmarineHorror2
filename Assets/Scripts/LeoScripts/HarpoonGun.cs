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
        ReloadAS.clip = ReloadClip;
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
            if (Harpoon == null)
            {
                Instantiate(HarpoonPrefab, transform.position, transform.rotation);
                FireSound();
            }
            if (Rope == null) 
            {
                Instantiate(RopePrefab, transform.position, transform.rotation);
            }
        }

        /*
        if (Input.GetMouseButtonUp(0)) 
        {
            Destroy(Harpoon);
            Destroy(Rope);
            LoadedHarpoon.SetActive(true);
            ReloadSound();
        }
        */
    }

    public void FireSound()
    {
        FireAS.Play();
    }

    public void HitSound()
    {
        HitAS.Play();
    }

    public void ReelInSound()
    {
        ReelInAS.Play();
    }

    public void StopReelInSound()
    {
        if (ReelInAS.isPlaying)
            ReelInAS.Stop();
    }

    public void ReloadSound()
    {
        ReloadAS.Play();
    }

    public void DeleteHarpoon()
    {
        Destroy(Harpoon);
        Destroy(Rope);
        LoadedHarpoon.SetActive(true);
        ReloadSound();
    }
}
