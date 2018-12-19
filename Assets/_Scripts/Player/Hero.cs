using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero active;

    public HeroData data;
    public Inventory inventory;
      private void Awake()
    {
        if (active != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            active = this;
        }

        DontDestroyOnLoad(active.gameObject);
        init();
    }


    /*
        INIT: initialize empty values in active hero before data is loaded/written

        there may be a more elegant solution for this but for now this is the space to 
        eliminate any null values because they will throw exceptions during deserialization
                -finch
     */
       public void init()
    {
        data.Name = "init";
        data.Level = 1;
        data.Atk = data.Def = data.Agi = data.Mag = 3;

        data.inventoryData = new InventoryData();
        inventory = new Inventory();
        inventory.items = new List<Item>();
    }

}