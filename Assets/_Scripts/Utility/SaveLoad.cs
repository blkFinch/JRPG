using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static void SaveHero()
    {

        //saves current inventory to HeroData as List<int> of item_ids
        Hero.active.inventory.SerializeInventory();

        //save current scene
       GameManager.gm.SaveCurrentScene();

        //serializes HeroData to local hard disk
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/heroData.gd");
        bf.Serialize(file, Hero.active.data);
        file.Close();
        Debug.Log("hero saved!");
    }

    //Here is all initializing functions to be performed after a load
    public static void LoadHero()
    {
        if (File.Exists(Application.persistentDataPath + "/heroData.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/heroData.gd", FileMode.Open);
            HeroData loadedHero = (HeroData)bf.Deserialize(file);
            file.Close();

            Hero.active.data = loadedHero;

            //Loads active inventory from HeroData
            Hero.active.inventory.DeserializeInventory();

            GameManager.gm.LoadSavedScene();

        }

    }
}