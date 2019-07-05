using UnityEngine.UI;

public class FilterButton : MenuButton<FilterButton>
{


    private void Awake() {
		Button _sBtn = this.GetComponent<Button>();
		_sBtn.onClick.AddListener(Filter);
	}



	private void Filter(){
		AudioManager.active.toggleFilter();
	}

}