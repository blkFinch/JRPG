using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Creature"))]
public class Creature : ScriptableObject {
	public string creatureName;
	public int atk, def, agi, hp;
	public int xp, gold;
	
	// TODO: consider adding more fields to creature:
	//
	// public string sk_primary, sk_secondary;
	// public int dmg_primary, dmg_secondary;
}
