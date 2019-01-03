using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour {

	CombatManager cm;

	public Button fleeButton;
	public Button atkBtn;
	// Use this for initialization
	void Start () {
		cm = FindObjectOfType<CombatManager>();

		fleeButton.onClick.AddListener(FleeCombat);
		atkBtn.onClick.AddListener(Attack);
	}

	void FleeCombat(){
		GameManager.gm.ReturnToWorld();
	}

	void Attack(){
		cm.PlayerAttack();
	}
	

}
