using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SampleButton : MenuButton<SampleButton>
{

    public Sample sample;
    private void Awake() {
        Button _sBtn = this.GetComponent<Button>();
        _sBtn.onClick.AddListener(PlaySample);
    }


    private void Start() {
        if(sample != null){
            Debug.Log("sample is not null");
            ToggleGraohic();
        }
    }

    private void ToggleGraohic(){
        GetComponentInChildren<Outline>().enabled = IsActive();
    }

    private bool IsActive(){
        return AudioManager.active.InstrumentIsPlaying(sample.instrument);
    }

    private void PlaySample() {
        
        if (!IsActive())
        {
            AudioManager.active.SpawnSlaveAudio(sample);
            ToggleGraohic();
        }
        else
        {
           AudioManager.active.DestroySalveAudio(sample);
           ToggleGraohic();
        }
    }

}