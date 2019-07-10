using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour {

	private GameObject pc;

	private void Awake() {
		pc = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {
		if(pc){
			//Starts player always at 0 height
			Vector3 startPos = new Vector3(transform.position.x, 0, transform.position.z); 
			pc.transform.position = startPos;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
