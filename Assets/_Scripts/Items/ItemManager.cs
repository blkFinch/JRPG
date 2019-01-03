using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public void UseEnergyDrink(){
		Debug.Log("Energy Drink used!");

		Hero.active.data.CurrentHp += 10;
	}
}
