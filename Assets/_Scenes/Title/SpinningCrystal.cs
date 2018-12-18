using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCrystal : MonoBehaviour {
	[SerializeField]
	private float rotationSpeedX = 10f;	
	private float rotationSpeedY = 3f;	
	void Update () {
		 // Rotate the object around its local Y axis at 1 degree per second
		transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeedX);
		transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeedY);
	}
}
