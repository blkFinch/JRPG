using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSampler : MonoBehaviour {

	public AudioClip clip;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = this.GetComponent<AudioSource>();
	}

	public void StartLoop(){
		source.clip = clip;
		source.loop = true;
		source.Play();
	}

	public void StopLoop(){
		source.loop = false;
	}
}
