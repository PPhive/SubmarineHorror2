using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{

    [SerializeField] private GameObject spawnPoint = null;
    [SerializeField] private GameObject boulderPrefab = null;
    [SerializeField] private float boulderScale = 1000f;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            GameObject boulder = Instantiate(boulderPrefab, spawnPoint.transform.position, Quaternion.identity);
            boulder.transform.localScale = new Vector3(Random.Range(.95f, 1.05f), Random.Range(.95f, 1.05f), Random.Range(.95f, 1.05f)) * boulderScale;
            boulder.GetComponent<Rigidbody>().AddForce(Vector3.down * 100f);
        }
    }

}
