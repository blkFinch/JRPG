using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject player;

	public float cameraMoveSpeed = 30f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	private void Update() {
		//Temp camera adjust tools
		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(Vector3.up * Time.deltaTime * cameraMoveSpeed); //this is unintuitive but for some reason i need to rotate on y
		}

		if(Input.GetKey(KeyCode.E)){
			transform.Rotate(Vector3.down * Time.deltaTime * cameraMoveSpeed);
		}
	}

	void LateUpdate(){
		transform.position = player.transform.position;

	}
}
