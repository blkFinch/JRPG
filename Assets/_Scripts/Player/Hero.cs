using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero active;

    public HeroData data;
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

       public void init()
    {
        data.Name = "init";
        data.Level = 1;
        data.Atk = data.Def = data.Agi = data.Mag = 3;
    }

}