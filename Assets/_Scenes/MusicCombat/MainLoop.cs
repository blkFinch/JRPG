using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour
{

    public AudioClip clip;
    public float clipLength;

    //INFO FOR MAIN LOOP
    public float bpm = 120f;
    public float barTime;
		float barsInClip;

    //Player Audio Samples
    public AudioSource slaveAudio;

    //Ambivalent notice that loop has started for other clips to sync
    public delegate void OnLoopStart();
    public event OnLoopStart notifyLoopStartObservers;
		public delegate void OnLastBar();
		public event OnLastBar notifyLastBarObservers;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();

        //esitmate bar time based on provided bpm [assumes 4:4]
        barTime = (60.0f / bpm) * 4.0f;

				LoadClip();
    }

		public void LoadClip(){
				source.clip = clip;
        clipLength = clip.length;
				barsInClip = clipLength/barTime;
        source.loop = true;
		}

	//Begins clip in a timed coroutine
    public void StartLoop()
    {

        StartCoroutine(playAndClock());
    }

    public void StopLoop()
    {
        source.loop = false;
    }

	//TODO: send delegate out on last bar
    IEnumerator playAndClock()
    {
        source.Play();
        while (source.isPlaying)
        {
						//start of loop
					int _bars = 0;
          if (notifyLoopStartObservers != null) { notifyLoopStartObservers();}

					while(_bars < barsInClip){
						if( _bars == (barsInClip - 1)){ 
							if(notifyLastBarObservers != null){notifyLastBarObservers();}
						}
						_bars++;
						yield return new WaitForSeconds(barTime);
					}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying)
        {
            slaveAudio.timeSamples = source.timeSamples;
        }
    }
}
