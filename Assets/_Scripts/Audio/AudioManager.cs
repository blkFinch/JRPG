using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager active;
    public static AudioSource masterAudio;

    public GameObject slaveAudioSourcePrefab;
    public List<GameObject> activeSlaves;

    public AudioMixerGroup filteredMix;
    private bool _filtered = false;
    //TODO: extract this out to it's own script -MasterClip
    //master loop to set time
    public AudioClip main;

    private void Awake() {
        if (active == null) { active = this; }
        else { Destroy(this.gameObject); }

        masterAudio = active.GetComponent<AudioSource>();

        activeSlaves = new List<GameObject>();

        InitMainLoop();
    }

    private void InitMainLoop() {
        masterAudio.clip = main;
        masterAudio.loop = true;
        masterAudio.Play();
    }

    public void SpawnSlaveAudio(Sample sample){
       slaveAudioSourcePrefab.GetComponent<SlaveAudio>().clipToPlay = sample.clip;
       GameObject _slave = Instantiate(slaveAudioSourcePrefab);
       activeSlaves.Add(_slave);
    }

    public void toggleFilter() {
        Debug.Log("toggle filter = " + _filtered);
        if (_filtered)
        {
            foreach(var _s in activeSlaves){
                _s.GetComponent<AudioSource>().outputAudioMixerGroup = null;
            }
            _filtered = false;
            masterAudio.outputAudioMixerGroup = null;
        }else{
             foreach(var _s in activeSlaves){
                _s.GetComponent<AudioSource>().outputAudioMixerGroup = filteredMix;
            }
            masterAudio.outputAudioMixerGroup = filteredMix;
            _filtered = true;
        }
    }

}
