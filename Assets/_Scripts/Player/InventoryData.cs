using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryData{
	public List<int> s_items;
	public List<int> s_samples;

	public void AddId(int item){
        if(s_items == null){s_items = new List<int>();}
		s_items.Add(item);
	}

	public void AddSampleId(int sample){
        if(s_samples == null){s_samples = new List<int>();}
		s_samples.Add(sample);
	}
}
