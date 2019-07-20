using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectSpawner : MonoBehaviour {

	public GameObject _object;
	private GameObject _activeObject;


	public static ScrollingObjectSpawner instance;

	[Header("Increase this to increase time between spawns")]
	public float xPositionDestory = 12f;
	public float speed;

	private void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//Spawns lamp after lamp destroy event
		if(_activeObject == null){
			SpawnObject();
		}
	}

	
	public void SpawnObject(){
		Quaternion _spawnRotation = _object.gameObject.transform.rotation;
		_activeObject = Instantiate(_object, transform.position, _spawnRotation);
		_activeObject.GetComponent<ScrollingObject>().xDestroyDistance = this.xPositionDestory;
		_activeObject.GetComponent<ScrollingObject>().parent = this;
		_activeObject.GetComponent<ScrollingObject>().scrollSpeed = this.speed;
	}

	private void Update() {
		if(_activeObject == null){
			SpawnObject();
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position,new Vector3(1, 1, 1));
	}
}
