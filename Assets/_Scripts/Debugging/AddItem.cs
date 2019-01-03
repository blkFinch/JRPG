using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{

    [SerializeField]
    private GameEvent OnEnergyDrinkUsed;

    public Item itemToAdd;

    public void DebAddItem()
    {
        Hero.active.inventory.AddItem(itemToAdd);
    }

    public void PrintInv()
    {
        OnEnergyDrinkUsed.Raise();
    }

	public void UseItem(){
		Item eDrink = GameManager.gm.itemDatabase.GetItem(1);
		Hero.active.inventory.UseItem(eDrink);
	}
}
