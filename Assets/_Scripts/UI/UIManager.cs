using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This manager is for mainly for use in world map/town map
//TODO: make combat UI manager for combat scene
public class UIManager : MonoBehaviour {

	private bool menuOpen = false;

	private void Start() {
		InputManager.im.notifyMenuButtonObservers += MenuButtonEvent;
	}

	private void MenuButtonEvent(){
		if(!menuOpen){ OpenMenu (); }
		else         { CloseMenu(); }
	}

	private void OpenMenu(){
		menuOpen = true;
		Debug.Log("menu opened");
	}

	private void CloseMenu(){
		menuOpen = false;
		Debug.Log("menu closed");
	}

}
