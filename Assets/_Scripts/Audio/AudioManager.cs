using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //TODO: refactor this to be handled by slider
    public Slider f_slider;
    public static AudioManager active;
    public static AudioSource masterAudio;

    public GameObject slaveAudioSourcePrefab;
    public List<GameObject> activeSlaves;

    public AudioMixerGroup filteredMix;
    private bool _filtered = false;

    private void Awake() {
        if (active != null) { Destroy(this.gameObject); }
        else { active = this; }
        DontDestroyOnLoad(active.gameObject);

        masterAudio = active.GetComponent<AudioSource>();
        activeSlaves = new List<GameObject>();
    }

    public void registerFilterSlifer(Slider slider) {
        f_slider = slider;
        f_slider.onValueChanged.AddListener(delegate { AdjustFilterCutoff(); });
    }
    private void OnDestroy() {
        Debug.Log("Audio Manager destroyed");
    }

    public void LaunchSample(Sample sample) {
        SpawnSlaveAudio(sample);
    }

    private void SpawnSlaveAudio(Sample sample) {
        slaveAudioSourcePrefab.GetComponent<SlaveAudio>().clipToPlay = sample.clip;
        slaveAudioSourcePrefab.GetComponent<SlaveAudio>().instrument = sample.instrument;
        GameObject _slave = Instantiate(slaveAudioSourcePrefab);
        _slave.transform.parent = this.transform;
        activeSlaves.Add(_slave);
        setMaster();
    }

    //Sets the master audio source to keep time
    private void setMaster() {
        if(activeSlaves.Count > 0){
            masterAudio = activeSlaves[0].GetComponent<AudioSource>();
        }
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
            setMaster();
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

    public void AdjustFilterCutoff() {
        Debug.Log("setting cutoff freq" + f_slider.value);
        filteredMix.audioMixer.SetFloat("cutoff", f_slider.value);
    }

}
