using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingLamp : ScrollingObject<ScrollingLamp> {


	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * (Time.deltaTime * scrollSpeed));
		if(this.transform.position.x > xDestroyDistance){
			Destroy(this.gameObject);
		}
	}
}
