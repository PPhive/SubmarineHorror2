using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public static Radar instance;
    public SphereCollider MyCollider;

    private void Awake()
    {
        instance = this;
        MyCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(ArrowPrefab,other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            BroadcastMessage("Exiting", other.gameObject);
        }
    }
}
