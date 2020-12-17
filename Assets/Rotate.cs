using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Transform me;
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        me.Rotate(rotation);
    }
}
