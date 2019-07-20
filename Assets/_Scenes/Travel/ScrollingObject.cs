using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    public float startPosX;
    public float endPosX;
    public float scrollSpeed;

    private Vector3 targetPos;
    private Vector3 startPos;

    private void Awake() {
        targetPos = new Vector3(endPosX, transform.position.y, transform.position.z);
        startPos = new Vector3(startPosX, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position != targetPos){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, scrollSpeed * Time.deltaTime);
        }else{
            this.transform.position = startPos;
        }
	}
}
