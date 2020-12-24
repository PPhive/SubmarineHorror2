using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAmbiance : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
