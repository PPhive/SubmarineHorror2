using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parascope : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Comma))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 30 * Time.deltaTime, transform.localEulerAngles.z);
        }
        else if (Input.GetKey(KeyCode.Period))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 30 * Time.deltaTime, transform.localEulerAngles.z);
        }
    }
}
