using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterSlider : MonoBehaviour {


	// Use this for initialization
	void Start () {
		AudioManager.active.registerFilterSlifer(this.GetComponent<Slider>());
	}
	
}
