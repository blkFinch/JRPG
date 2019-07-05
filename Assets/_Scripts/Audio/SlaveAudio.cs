using UnityEngine;

public class SlaveAudio : MonoBehaviour {

	public AudioClip clipToPlay;
	private AudioSource slave;

	private void Awake() {
		Debug.Log("Slave Audio awake");
		slave = GetComponent<AudioSource>();
		Debug.Log("Slave AS: " +  slave);
		slave.clip = clipToPlay;
		slave.loop = true;
		slave.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(slave.isPlaying){
			slave.timeSamples = AudioManager.masterAudio.timeSamples;
		}
	}
}
