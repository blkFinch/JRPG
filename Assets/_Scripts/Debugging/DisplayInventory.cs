using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//DEBUGGING  SCRIPT
//
//script is for displaying a quick and dirty list of player items
public class DisplayInventory : MonoBehaviour {
	Text text;
	Inventory _inv;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		_inv = Hero.active.inventory;
	}

	string InventoryString(){
		string _s = "";
		foreach(Item item in _inv.items){
			_s += item.Name + "\n";
		}
		return _s;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = InventoryString();
		
	}
}
