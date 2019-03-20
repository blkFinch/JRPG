using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHpDisplay : MonoBehaviour {


	Text text;
	string currentHp;
    string totalHp;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		currentHp = Hero.active.data.CurrentHp.ToString();
        totalHp = Hero.active.data.Hp.ToString();
		
		text.text = "HP: " + currentHp + " / " + totalHp;
	}
}