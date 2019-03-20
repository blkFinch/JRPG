using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour {

	public AudioClip clip;
	public float clipLength;

	public AudioSource slaveAudio;

	//Ambivalent notice that loop has started for other clips to sync
	public delegate void OnLoopStart();
	public event OnLoopStart notifyLoopStartObservers;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = this.GetComponent<AudioSource>();
	}

	public void StartLoop(){
		source.clip = clip;
		clipLength = clip.length;
		source.loop = true;
	
		StartCoroutine(playAndClock());
	}

	public void StopLoop(){
		source.loop = false;
	}

	IEnumerator playAndClock(){
		source.Play();
		while(source.isPlaying){
			Debug.Log("start loop");
			
			if(notifyLoopStartObservers != null){ notifyLoopStartObservers(); }
			yield return new WaitForSeconds(clipLength);
		}
	}
	
	// Update is called once per frame
	void Update () {
		slaveAudio.timeSamples = source.timeSamples;

	}
}
