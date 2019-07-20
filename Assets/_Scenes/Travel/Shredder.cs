using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("trigger");
            Destroy(other.gameObject);

			ScrollingObjectSpawner.instance.SpawnObject();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
