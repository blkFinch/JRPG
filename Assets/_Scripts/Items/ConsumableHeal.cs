using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Use Heal", menuName = "RPG/ConsumableActions/HealingAction")]
public class ConsumableHeal : ConsumableAction {
	[SerializeField]
	string onUseString;

	[SerializeField]
	int AmountToHeal;
	public override void OnConsume(){
		Hero.active.data.CurrentHp += AmountToHeal;

		Debug.Log(onUseString);
	}
}
