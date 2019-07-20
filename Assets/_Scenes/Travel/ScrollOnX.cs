using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollOnX : ScrollingObject {

	

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * (Time.deltaTime * scrollSpeed));
		if(this.transform.position.x >= xDestroyDistance){
			if(parent != null){parent.SpawnObject();}
			Destroy(this.gameObject);
		}
	}
}
