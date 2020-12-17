using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parascope : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftBracket))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 20 * Time.deltaTime, transform.localEulerAngles.z);
        }
        else if (Input.GetKey(KeyCode.RightBracket))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 20 * Time.deltaTime, transform.localEulerAngles.z);
        }
    }
}
