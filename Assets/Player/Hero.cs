using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hero : MonoBehaviour
{
    //fields
    public static Hero current;
    private int _level;
    private string _name;


    //make singleton
    private void Awake()
    {
        if (current != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            current = this;
        }

        DontDestroyOnLoad(current.gameObject);
    }
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
    //construct new
    public Hero()
    {
        this._level = 1;
        this._name = "Null name";
        Debug.Log("hero created!");
    }

    //methods here
    public void LoadData(HeroData data){
        this._name = data.name;
        this._level = data.level;
    }

}