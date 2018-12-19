using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData
{
    public InventoryData inventoryData;
    private int _level;
    private string _name;

    int atk, def, agi, mag;

    int hp, xp;

    //properties
    public int Level
    {
        get { return _level; }
        set
        {
            if (value > 1) { _level = value; }
        }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Atk
    {
        get
        {
            return atk;
        }

        set
        {
            atk = value;
        }
    }

    public int Def
    {
        get
        {
            return def;
        }

        set
        {
            def = value;
        }
    }

    public int Agi
    {
        get
        {
            return agi;
        }

        set
        {
            agi = value;
        }
    }

    public int Mag
    {
        get
        {
            return mag;
        }

        set
        {
            mag = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    public int Xp
    {
        get
        {
            return xp;
        }

        set
        {
            xp = value;
        }
    }

}