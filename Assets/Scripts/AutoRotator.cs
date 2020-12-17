using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotSpeed = new Vector3(0, 5, 0);

    private void FixedUpdate()
    {
        transform.localEulerAngles += rotSpeed * Time.fixedDeltaTime;
    }
}
