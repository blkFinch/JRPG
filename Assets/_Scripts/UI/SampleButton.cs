using UnityEngine.UI;

public class SampleButton : MenuButton<SampleButton>
{

    public Sample sample;
    private void Awake() {
		Button _sBtn = this.GetComponent<Button>();
		_sBtn.onClick.AddListener(PlaySample);
	}



	private void PlaySample(){
		AudioManager.active.SpawnSlaveAudio(sample);
	}

}