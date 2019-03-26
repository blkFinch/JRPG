using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingIcon : MonoBehaviour {

	public float speed = 1f;

	public void setSpeed(float _speed){
		this.speed = _speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.gameObject.transform.position -= Vector3.right * speed;

		//DESTROY UNUSED
		if(this.transform.position.x < -15f ){
			Destroy(this.gameObject);
		}
		
	}
}
