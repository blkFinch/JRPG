using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroLevelDisplay : MonoBehaviour {


	Text text;
	string level;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		level = Hero.active.data.Level.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
		text.text = "Lv: " + level;
	}
}
