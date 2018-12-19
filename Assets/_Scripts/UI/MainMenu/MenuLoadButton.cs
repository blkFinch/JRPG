using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuLoadButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

		public Text display;
	public void OnSelect(BaseEventData eventData){
        
       display.text = "Load Game? You will lose unsaved data...";
	}

	//clears the text display
	public void OnDeselect(BaseEventData eventData){
		display.text = " ";
	}

	public void OnClick(){
		SaveLoad.LoadHero();
	}
}