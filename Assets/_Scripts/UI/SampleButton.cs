using UnityEngine.UI;

public class SampleButton : MenuButton<SampleButton>
{

    public Sample sample;
    private void Awake() {
        Button _sBtn = this.GetComponent<Button>();
        _sBtn.onClick.AddListener(PlaySample);
        ToggleGraohic();
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