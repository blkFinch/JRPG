using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplerButton : MenuButton<SamplerButton>
{
    private void Awake() {
		Button _sBtn = this.GetComponent<Button>();
		_sBtn.onClick.AddListener(OpenSamplerMenu);
	}

    // Use this for initialization
    void Start() {
        //This checks for sampler TODO: extract this to a KeyItemChecker script
        if (Hero.active.inventory.HasItem(2))
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }


	private void OpenSamplerMenu(){
		MenuManager.Instance.OpenSamplerMenu();
	}

}
