using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour {

	CombatManager cm;

	private SelectOnInput soi;

	public Button fleeButton;
	public Button atkBtn;
	
	// Use this for initialization
	void Start () {
		cm = FindObjectOfType<CombatManager>();

		fleeButton.onClick.AddListener(FleeCombat);
		atkBtn.onClick.AddListener(Attack);
	}

	public void PlayerTurn(){
		soi = GetComponent<SelectOnInput>();
		Debug.Log("objects:" + soi + atkBtn);
		soi.selectedObject = atkBtn.gameObject;
	}

	void FleeCombat(){
		GameManager.gm.ReturnToWorld();
	}

	void Attack(){
		if(InputManager.im.inputMode != InputMode.CombatTarget){
			InputManager.im.InputModeTarget();
			cm.Target(0); //init target to 0
		}
	}
	

}
