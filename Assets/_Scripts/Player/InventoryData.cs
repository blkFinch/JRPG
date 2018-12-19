using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData{
	public List<int> s_items;

	public void AddId(int item){
        if(s_items == null){s_items = new List<int>();}
		s_items.Add(item);
	}
}
