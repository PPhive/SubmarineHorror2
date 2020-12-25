using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatue : MonoBehaviour
{
    public EnemyBaby MyBaby;
    public GameObject BabyPrefab;
    public GameObject CordPrefab;
    public Rigidbody RB;
    public Transform[] PatrolNodes;     //If you have any PatrolNodes,the baby you spawn will automatically Patrol, else it stays static.
    [Space]
    public float SpawnRange;

    void Start()
    {
        SpawnBaby();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyBaby == null) 
        {
            if (Vector3.Distance(transform.position, CallPlayerRigidBody.instance.transform.position) > SpawnRange) 
            {
                SpawnBaby();
            }
        }
    }

    public void SpawnBaby() 
    {
        Instantiate(BabyPrefab, transform);
        Instantiate(CordPrefab, transform);
        //Instantiate(BabyPrefab, transform.position + new Vector3(0, 0, 1), transform.rotation);
    }
}
