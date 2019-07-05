using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MenuButton<LoadButton> {
	private Button _loadBtn;

	private void Awake() {
		_loadBtn = this.GetComponent<Button>();
		_loadBtn.onClick.AddListener(LoadGame);
	}

	private void LoadGame(){
		SaveLoad.LoadHero();
	}
	
}
