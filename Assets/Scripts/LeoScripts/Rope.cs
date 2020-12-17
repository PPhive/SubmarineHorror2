using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject RopeBody;
    private float Distance;

    void Awake()
    {
        HarpoonGun.instance.Rope = this.gameObject;
        transform.LookAt(HarpoonGun.instance.Harpoon.transform);
        RopeBody.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(transform.position, HarpoonGun.instance.Harpoon.transform.position));
        RopeBody.transform.position = HarpoonGun.instance.transform.position;
    }

    void Update()
    {
        Distance = Vector3.Distance(transform.position, HarpoonGun.instance.Harpoon.transform.position);

        transform.position = HarpoonGun.instance.transform.position;
        transform.LookAt(HarpoonGun.instance.Harpoon.transform);
        RopeBody.transform.localScale = new Vector3(0.025f, 0.025f,Distance/2);
        //RopeBody.transform.localPosition = new Vector3 (0,0,Distance/2);
    }
}
