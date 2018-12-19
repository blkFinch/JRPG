using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

  //TODO: make this generate an item button prefab for each item on list
	public Text itemList;
	public void OnSelect(BaseEventData eventData){
        
        foreach(Item item in Hero.active.inventory.items){
            itemList.text = itemList.text + "\n\n" + item.name;
        }
	}

	//clears the text display
	public void OnDeselect(BaseEventData eventData){
		itemList.text = " ";
	}

}