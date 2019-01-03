using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Creature"))]
public class Creature : ScriptableObject {
	[SerializeField]
	private string creatureName;

	[SerializeField]
	private int atk, def, agi, hp;

	[SerializeField]
	private int xp, gold;

    private int currentHp;

    public int Atk
    {
        get
        {
            return atk;
        }
    }

    public int Def
    {
        get
        {
            return def;
        }

    }

    public int Agi
    {
        get
        {
            return agi;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

    }

    public int Xp
    {
        get
        {
            return xp;
        }

    }

    public int Gold
    {
        get
        {
            return gold;
        }

    }

    public string CreatureName
    {
        get
        {
            return creatureName;
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
        }
    }

    // TODO: consider adding more fields to creature:
    //
    // public string sk_primary, sk_secondary;
    // public int dmg_primary, dmg_secondary;

    public void init() {
        Debug.Log(creatureName + " awake!");
        currentHp = hp;
    }

    private void OnDestroy() {
        Debug.Log(creatureName + " destroyed!");
    }
}
