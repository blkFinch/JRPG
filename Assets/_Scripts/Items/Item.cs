using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Item"))]
public class Item : ScriptableObject
{

    public ConsumableAction onUseItem;
    [SerializeField]
    private string i_Name;

    [SerializeField]
    private int i_Value;

    [SerializeField]
    private bool isKeyItem;

    [SerializeField]
    [Header("Never set id to 0! IDs must be unique")]
    private int i_Id;

    public string Name
    {
        get { return i_Name; }
    }

    public int Value
    {
        get { return i_Value; }
    }

    public bool IsKeyItem
    {
        get { return isKeyItem; }
    }


    public int ID
    {
        get
        {
            return i_Id;
        }
    }
}
