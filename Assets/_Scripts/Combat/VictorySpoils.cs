using UnityEngine;
using System.Collections.Generic;

public class VictorySpoils{

    private int v_gold, v_xp;
    public VictorySpoils(List<Creature>DefeatedFoes){
        foreach(Creature foe in DefeatedFoes ){
            this.v_gold += foe.Gold;
            this.v_xp += foe.Xp;
        }
    }

    public string toString(){
        return "gold: " +  v_gold + " xp: " + v_xp;
    }

    public int Gold
    {
        get
        {
            return v_gold;
        }

        set
        {
            v_gold = value;
        }
    }

    public int Xp
    {
        get
        {
            return v_xp;
        }

        set
        {
            v_xp = value;
        }
    }
}