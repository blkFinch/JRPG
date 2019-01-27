﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{
	private InventoryData inventoryData;
	public List<Item> items;

	public void AddItem(Item item){
		items.Add(item);
	}

	public void UseItem(Item item){
		item.onUseItem.OnConsume();
		items.Remove(item);
	}

	//SERIALIZATION 
	//TODO: catch null reference exceptions during serial/deserial
	public void SerializeInventory(){
		inventoryData = Hero.active.data.inventoryData;

		foreach (var item in items)
		{
			inventoryData.AddId(item.ID);
		}
	}

	public void DeserializeInventory(){
		inventoryData = Hero.active.data.inventoryData;

		//dealing with weirdness from changing the save script this should never happen in production
		// TODO: look at this later
		if(inventoryData.s_items == null){ inventoryData.s_items = new List<int>(); } 

		Item itemToAdd;

		foreach (var id in inventoryData.s_items)
		{
			itemToAdd = GameManager.gm.itemDatabase.GetItem(id);
			this.AddItem(itemToAdd);
		}
	}


}