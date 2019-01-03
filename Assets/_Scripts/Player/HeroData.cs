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

    int gold;

    int hp, xp;

    int currentHp;

    float loc_X, loc_Y, loc_Z;

    //properties
    public int Level
    {
        get { return _level; }
        set
        {
             _level = value; 
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

    public float Loc_X
    {
        get
        {
            return loc_X;
        }

        set
        {
            loc_X = value;
        }
    }

    public float Loc_Y
    {
        get
        {
            return loc_Y;
        }

        set
        {
            loc_Y = value;
        }
    }

    public float Loc_Z
    {
        get
        {
            return loc_Z;
        }

        set
        {
            loc_Z = value;
        }
    }

    public int CurrentHp
    {
        get
        {
            return currentHp;
        }

        set
        {
            currentHp = value;
            if (currentHp > hp){currentHp = hp;}
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }

        set
        {
            gold = value;
        }
    }
}