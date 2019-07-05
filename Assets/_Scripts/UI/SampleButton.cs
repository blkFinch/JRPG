using UnityEngine.UI;

public class SampleButton : MenuButton<SampleButton>
{

    public Sample sample;
    private bool isActive = false;
    private void Awake() {
        Button _sBtn = this.GetComponent<Button>();
        _sBtn.onClick.AddListener(PlaySample);
    }



    private void PlaySample() {
        if (!isActive)
        {
            AudioManager.active.SpawnSlaveAudio(sample);
			isActive = true;
        }
        else
        {
           AudioManager.active.DestroySalveAudio(sample);
		   isActive = false;
        }
    }

}