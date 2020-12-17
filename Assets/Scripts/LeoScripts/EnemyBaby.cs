using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaby : MonoBehaviour
{
    public bool Patrolling;
    public int PatrolCounter;
    [Space]
    public bool Attacking; //If the baby is charging
    public Rigidbody MyRB;
    public EnemyStatue MyStatue;
    public SpringJoint MyJoint;

    [Space]
    public GameObject StaticStance;
    public GameObject SwimmingStance;
    [Space]
    public float DistanceToPlayer;
    public bool Streched;
    public float SearchRange; //When within this range, baby would start attacking if not out of the cord's range.
    public float SearchRangeClose; //When within this range, baby would attack even if being pulled by the cord, 

    void Start()
    {
        MyStatue = transform.parent.GetComponent<EnemyStatue>();
        MyStatue.MyBaby = this;
        transform.parent = null;
        MyJoint.connectedBody = MyStatue.RB;

    }

    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, CallPlayerRigidBody.instance.transform.position);

        if (DistanceToPlayer < SearchRangeClose)
        {
            Attacking = true;
        }
        else if (DistanceToPlayer < SearchRange && !Streched)
        {
            Attacking = true;
        }
        else
        {
            Attacking = false;
        }

        if (Vector3.Distance(transform.position, MyStatue.transform.position) > MyJoint.maxDistance - 1) 
        {
            Streched = true;
        }
        else 
        {
            Streched = false;
        }


        if (MyStatue.PatrolNodes.Length > 0)
        {
            Patrolling = true;
        }
        else 
        {
            Patrolling = false;
        }


        if (Patrolling && !Attacking) 
        {
            transform.LookAt(MyStatue.PatrolNodes[PatrolCounter]);
            MyRB.AddForce(transform.forward * 1);
            if (Vector3.Distance(transform.position, MyStatue.PatrolNodes[PatrolCounter].position) < 0.2f) 
            {
                PatrolCounter += 1;
            }
            if (PatrolCounter >= MyStatue.PatrolNodes.Length) 
            {
                PatrolCounter = 0;
            }
        }

        if (Attacking) 
        {
            transform.LookAt(PlayerManager.instance.transform);
        }

        if (Attacking || Patrolling)
        {
            SwimmingStance.SetActive(true);
            StaticStance.SetActive(false);
        }
        else 
        {
            StaticStance.SetActive(true);
            SwimmingStance.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Attacking && collision.gameObject.tag == "Player") 
        {
            Debug.Log("Attacked Player");
            MyRB.AddForce(transform.forward * - 200);
            //Call Player take damage method
        }
    }

    private void FixedUpdate()
    {
        if (Attacking)
        {
            MyRB.AddForce(transform.forward * 2);
        }
    }

}
