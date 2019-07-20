using System.Collections.Generic;
using System.Linq;

/*
	For now inventory holds all items and samples. HeroData holds all information
	about hero, and inventoryData holds all information about items/equipment/samples
	anything that can be added or removed from hero...
 */
//TODO: consider splitting this into two scipts one for items one for samples

public class Inventory
{
    //InventoryData is a serializable class to save all inventory 
    private InventoryData inventoryData;

    public List<Item> items;
    public List<Sample> samples;

    public void AddItem(Item item) {
        items.Add(item);
    }

    public void AddSample(Sample sample) {
        //Adds sample only if not already in inventory
        bool _presentInList = samples.Any(item => item.Id == sample.Id);
        if (!_presentInList) { samples.Add(sample); }
    }

    public void UseItem(Item item) {
        item.onUseItem.OnConsume();
        items.Remove(item);
    }

    public bool HasItem(int id) {
        foreach (Item item in items)
        {
            if (item.ID == id) { return true; }
        }
        return false;
    }

    public bool HasSample(int id) {
        foreach (Sample sample in samples)
        {
            if (sample.Id == id) { return true; }
        }
        return false;
    }

    //SERIALIZATION 
    public void SerializeInventory() {
        inventoryData = Hero.active.data.inventoryData;

        //saves the ids of all items
        foreach (var item in items) { inventoryData.AddId(item.ID); }

        //saves the ids of all samples
        foreach (var sample in samples) { inventoryData.AddSampleId(sample.Id); }
    }

    public void DeserializeInventory() {
        inventoryData = Hero.active.data.inventoryData;

        //dealing with weirdness from changing the save script this should never happen in production
        // TODO: look at this later
        if (inventoryData.s_items == null) { inventoryData.s_items = new List<int>(); }

        foreach (var id in inventoryData.s_items)
        {
            AddItemFromID(id);
        }

        foreach (var sample_id in inventoryData.s_samples)
        {
            AddSampleFromID(sample_id);
        }
    }

    public void AddItemFromID(int id) {
        Item itemToAdd = GameManager.gm.itemDatabase.GetItem(id);
        if (itemToAdd != null) { this.AddItem(itemToAdd); }
    }

    public void AddSampleFromID(int id) {
        Sample sampleToAdd = GameManager.gm.sampleDatabse.GetSample(id);
        if (sampleToAdd != null) { this.AddSample(sampleToAdd); }
    }


}
