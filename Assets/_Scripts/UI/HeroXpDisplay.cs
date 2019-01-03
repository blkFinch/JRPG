using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroXpDisplay : MonoBehaviour {


	Text text;
	string xp;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		xp = Hero.active.data.Xp.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
		text.text = "xp: " + xp;
	}
}