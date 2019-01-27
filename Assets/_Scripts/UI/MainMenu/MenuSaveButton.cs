using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSaveButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public Text display;

	public delegate void MenuSaveSelected();
	public event MenuSaveSelected notifyMenuTabObservers;
    public void OnSelect(BaseEventData eventData)
    {

		notifyMenuTabObservers();

        display.text = "Save Game? This will overwrite previous save...";
    }

    //clears the text display
    public void OnDeselect(BaseEventData eventData)
    {
        display.text = " ";
    }

    public void OnClick()
    {
        SaveLoad.SaveHero();
    }
}
