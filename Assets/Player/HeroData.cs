using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroData {
	public int level;
	public string name;
	public HeroData(){
		this.level = Hero.current.Level;
		this.name = Hero.current.Name;
	}
}
