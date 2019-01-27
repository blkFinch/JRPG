using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public enum InputMode{DirectControl, DialogueSelect, MenuSelect} 

public class InputManager : MonoBehaviour {

	
	public InputMode inputMode = InputMode.DirectControl;
	
	public delegate void OnActionButton(); //action button delegate will fire whenever 'Action Button' is hit 
	public event OnActionButton notifyActionButtonObservers;

	public delegate void OnMenuButton(); //this for menu button and info maybe??
	public event OnMenuButton notifyMenuButtonObservers;

	public delegate void OnCancelButton(); //pressing the 'b' button
	public event OnCancelButton notifyCancelButtonObservers;

	//SINGLETON
	public static InputManager im;
    private void Awake()
    {
        if (im != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            im = this;
        }

        DontDestroyOnLoad(im.gameObject);
    }

	//Shortcuts for other Managers
	GameManager gm;
	DialogueManager dm;

	//Catches
	private bool axisInUse = false; //so only one choice can be selected at a time

	// Use this for initialization
	void Start () {
		gm = GameManager.gm;
		dm = DialogueManager.active;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Return) || CrossPlatformInputManager.GetButtonDown("Fire1")){
			if(notifyActionButtonObservers != null ){ notifyActionButtonObservers(); }
		}

		if(CrossPlatformInputManager.GetButtonDown("Fire3")){
			if(notifyMenuButtonObservers != null ){ notifyMenuButtonObservers(); }
		}

		if(CrossPlatformInputManager.GetButtonDown("Fire2")){
			if(notifyCancelButtonObservers != null ){ notifyCancelButtonObservers(); }
			if( inputMode == InputMode.DialogueSelect){ dm.ExitDialogue(); } 
		}

		if( inputMode == InputMode.DirectControl ){ ProcessMovement(); }
		else if( inputMode == InputMode.DialogueSelect){ ProcessDialogueSelect(); }	
	}

	//INPUT STATE MANAGEMENT
	//TODO: this state management might be overkill here so let's reveevaluate after I complete combat
	public void InputModeDirect(){
		inputMode = InputMode.DirectControl;
	}

	public void InputModeDialogue(){
		inputMode = InputMode.DialogueSelect;
	}

	public void InputModeMenu(){
		inputMode = InputMode.MenuSelect;
	}

	void ProcessMovement(){
		
	}

	void ProcessDialogueSelect(){
		float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
		

		if(v == 1){
			if(axisInUse == false){
				axisInUse = true;
				dm.NavigateUp();
			}	
				
		}

		if(v == -1){
			if(axisInUse == false){
				axisInUse = true;
				dm.NavigateDown();
			}	
		}

		if(v == 0 ){
			axisInUse = false;
		}
	}

}
