using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{

    public float speed = 0.1f;


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision detected");
        if (other.gameObject.tag == "Shredder")
        {
            Debug.Log("Shredded");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        MCombatManager.mcom.canScore = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        MCombatManager.mcom.canScore = false;
    }

    // Update is called once per frame
    void Update()
    {
     
    	this.gameObject.transform.position += Vector3.right * speed;
        

    }

}
