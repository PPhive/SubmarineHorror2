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

    void Awake() 
    {
        instance = this;
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
        }

    }
}
