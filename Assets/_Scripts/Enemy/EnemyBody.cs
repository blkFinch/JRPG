using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

	public int agi, atk, def, maxHp, currentHp;
	public string _name;


	public Sprite sprite;

	public Creature creatureTemplate;

	private CombatManager cm;

	public void init(Creature creature) {
        Debug.Log(creature.name + " awake!");
        agi = creature.Agi;
		atk = creature.Atk;
		def = creature.Def;
		maxHp = creature.Hp;
		currentHp = maxHp;
		sprite = creature.Sprite;
		creatureTemplate = creature;

		cm = FindObjectOfType<CombatManager>();
    }


	public void TakeDamage(int damage){
		this.currentHp -= damage;
		if(this.currentHp <= 0){ KnockOut();}
	}

	//TODO: i hate how entangled this is with combatmanager...
	//consider moving this back into the combat manager
	public void KnockOut(){
		cm.defeatedEnemies.Add(this.creatureTemplate);
		cm.turnOrder.Remove(this.gameObject);
		Destroy(this.gameObject);
	}

	private void OnDestroy() {
        Debug.Log(name + " destroyed!");
    }

}
