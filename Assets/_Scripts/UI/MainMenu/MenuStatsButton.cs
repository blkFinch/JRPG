using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuStatsButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	//TODO: make this generate a proper stats panel
	public Text statsDisplay;

	public delegate void MenuStatsSelected();
	public event MenuStatsSelected notifyStatsTabObservers;

	public void OnSelect(BaseEventData eventData){
		statsDisplay.text = Stats();
		notifyStatsTabObservers();
	}

	//clears the text display
	public void OnDeselect(BaseEventData eventData){
		statsDisplay.text = " ";
	}

	private string Stats(){
		return         "HP: "  + Hero.active.data.CurrentHp + " / " + Hero.active.data.Hp +
			       "\n\nAtk: " + Hero.active.data.Atk +
			       "\n\nDef: " + Hero.active.data.Def +
			       "\n\nAgi: " + Hero.active.data.Def +
			       "\n\nMag: " + Hero.active.data.Mag;
	}
}
