using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData
{
    private int _level;
    private string _name;

    int atk, def, agi, mag;

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

}