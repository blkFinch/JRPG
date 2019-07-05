using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsCamera : MonoBehaviour {

	public Transform mainCamera;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(mainCamera);
	}
}
