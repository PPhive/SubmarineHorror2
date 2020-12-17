using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPlayerRigidBody : MonoBehaviour
{
    public static CallPlayerRigidBody instance;
    public Rigidbody RB;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RB = gameObject.GetComponent<Rigidbody>();
    }
}
