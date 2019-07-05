using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: remove later
using UnityStandardAssets.CrossPlatformInput;

public class MovingIcon : MonoBehaviour {

	public float speed = 1f;
	public float TARGET_X_POS = -6f;

	public void setSpeed(float _speed){
		this.speed = _speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.gameObject.transform.position -= Vector3.right * speed;

		if(CrossPlatformInputManager.GetButtonDown("Fire1")){
			if(this.gameObject.transform.position.x > (TARGET_X_POS - 1f) && this.gameObject.transform.position.x <(TARGET_X_POS + 1f)){
				Debug.Log("succesful button");
				Destroy(this.gameObject);
			}
		}

		//DESTROY UNUSED
		if(this.transform.position.x < -15f ){
			Destroy(this.gameObject);
		}
		
	}
}
