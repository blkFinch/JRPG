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
        if (active != null) { Destroy(this.gameObject); }
        else { active = this; }
        DontDestroyOnLoad(active.gameObject);

        masterAudio = active.GetComponent<AudioSource>();
        activeSlaves = new List<GameObject>();

        InitMainLoop();
    }

    private void OnDestroy() {
        Debug.Log("Audio Manager destroyed");
    }

    private void InitMainLoop() {
        masterAudio.clip = main;
        masterAudio.loop = true;
        masterAudio.Play();
    }

    public void SpawnSlaveAudio(Sample sample) {
        slaveAudioSourcePrefab.GetComponent<SlaveAudio>().clipToPlay = sample.clip;
        slaveAudioSourcePrefab.GetComponent<SlaveAudio>().instrument = sample.instrument;
        GameObject _slave = Instantiate(slaveAudioSourcePrefab);
        _slave.transform.parent = this.transform;
        activeSlaves.Add(_slave);
    }

    public void DestroySalveAudio(Sample sample) {
        GameObject _sToDestroy = null;
        foreach (var _s in activeSlaves)
        {
            if (sample.clip == _s.GetComponent<SlaveAudio>().clipToPlay)
            {
                _sToDestroy = _s;
            }
        }
        if (_sToDestroy != null)
        {
            activeSlaves.Remove(_sToDestroy);
            Destroy(_sToDestroy);
        }
    }

    public bool InstrumentIsPlaying(Instrument _instrument) {
        Debug.Log("Is instrument playing>" + _instrument);
        foreach (var sample in activeSlaves)
        {
            if (sample.GetComponent<SlaveAudio>().instrument == _instrument) { return true; }
        }
        return false;
    }

    public void toggleFilter() {
        Debug.Log("toggle filter = " + _filtered);
        if (_filtered)
        {
            foreach (var _s in activeSlaves)
            {
                _s.GetComponent<AudioSource>().outputAudioMixerGroup = null;
            }
            _filtered = false;
            masterAudio.outputAudioMixerGroup = null;
        }
        else
        {
            foreach (var _s in activeSlaves)
            {
                _s.GetComponent<AudioSource>().outputAudioMixerGroup = filteredMix;
            }
            masterAudio.outputAudioMixerGroup = filteredMix;
            _filtered = true;
        }
    }

}
