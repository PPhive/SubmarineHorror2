using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaManager : MonoBehaviour
{

    [SerializeField] private float speed = 1f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.up * Time.fixedDeltaTime * speed);
        if (PlayerManager.instance.transform.position.y < transform.position.y)
        {
            PlayerManager.instance.LavaTouch();
        }
    }
}
