using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour {

	public Item itemToAdd;

	public void DebAddItem(){
		Hero.active.inventory.AddItem(itemToAdd);
	}

	public void PrintInv(){
		Debug.Log("items: " + Hero.active.inventory.items[0].Name);
	}
}
