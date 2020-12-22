using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarArrows : MonoBehaviour
{
    public GameObject Target;
    private EnemyBaby TargetScript;

    public GameObject Arrow;
    private float MaxDistance;

    private void Awake()
    {
        Target = transform.parent.gameObject;
        transform.parent = Radar.instance.transform;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        TargetScript = Target.GetComponent<EnemyBaby>();
        MaxDistance = Vector3.Distance(transform.position, Target.transform.position);
    }

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.LookAt(Target.transform);
        if (!TargetScript.isActiveAndEnabled)
        {
            Destroy(this.gameObject);
        }

        //Arrow Scaling with distance;
        Arrow.transform.localScale = new Vector3(2, 2, 2) * (1 - (Vector3.Distance(transform.position, Target.transform.position)/MaxDistance));
    }

    void Exiting(GameObject ExitingBaby)
    {
        if (ExitingBaby == Target)
        {
            Destroy(this.gameObject);
        }
    }
}
