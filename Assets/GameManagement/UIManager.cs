using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This manager is for mainly for use in world map/town map
//TODO: make combat UI manager for combat scene
public class UIManager : MonoBehaviour {

	public Button _menuButton;
	public Canvas _menuCanvas;
	public Canvas _wmHud;
	public bool _menuActive = false;


	// Use this for initialization
	void Start () {
		_menuButton.onClick.AddListener(ToggleMenu);
	}
	void ToggleMenu(){
		_menuActive = !_menuActive;
		_menuCanvas.gameObject.SetActive(_menuActive);
		_wmHud.gameObject.SetActive(_menuActive);
	}


}
