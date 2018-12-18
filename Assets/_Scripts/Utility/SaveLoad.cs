using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad{

    public static void SaveHero(){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/heroData.gd");
        bf.Serialize(file, Hero.active.data);
        file.Close();
        Debug.Log("hero saved!");
    }

    public static void LoadHero() {
    if(File.Exists(Application.persistentDataPath + "/heroData.gd")) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/heroData.gd", FileMode.Open);
        HeroData loadedHero = (HeroData)bf.Deserialize(file);
        file.Close();

        Hero.active.data = loadedHero;
    }
}
}