using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, ISelectHandler {

  //TODO: better system for setting location to insantiate buttons
	public GameObject initSlot;
	public GameObject btnHolder;
	public Button itemButton;
	public void OnSelect(BaseEventData eventData){
        Debug.Log("inventory selected");
		Debug.Log(Hero.active.inventory.items);

		RefreshItemList();
	}

	//TODO: consider refactoring this out to the main menu script
	public void RefreshItemList()
    {
        ClearItemButtons();

        foreach (Item item in Hero.active.inventory.items)
        {
            Button itmBtn = Instantiate(itemButton, initSlot.transform.position, Quaternion.identity);
            itmBtn.transform.parent = btnHolder.transform;
            itmBtn.GetComponentInChildren<Text>().text = item.Name;
            itmBtn.onClick.AddListener(() => UseSelectedItem(item));
        }
    }

    public void ClearItemButtons()
    {
        int childCount = btnHolder.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(btnHolder.transform.GetChild(i).gameObject);
        }
    }

    void UseSelectedItem(Item item){
		Debug.Log("item button clicked");
		Hero.active.inventory.UseItem(item);
		RefreshItemList();
	}

}