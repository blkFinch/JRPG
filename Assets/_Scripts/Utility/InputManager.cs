using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public enum InputMode { DirectControl, DialogueSelect, MenuSelect, CombatTarget }

public class InputManager : MonoBehaviour
{

    //Accessors for inputs
    public InputMode inputMode = InputMode.DirectControl;
    public bool isWalking;
    public bool canWalk { get; private set; }
    float _v; //vertical axis
    float _h; //horizontal axis


    public delegate void OnActionButton(); //action button delegate will fire whenever 'Action Button' is hit 
    public event OnActionButton notifyActionButtonObservers;

    public delegate void OnMenuButton(); //this for menu button and info maybe??
    public event OnMenuButton notifyMenuButtonObservers;

    public delegate void OnCancelButton(); //pressing the 'b' button
    public event OnCancelButton notifyCancelButtonObservers;

    //SINGLETON
    public static InputManager im;
    private void Awake() {
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
    CombatManager cm;

    //Catches
    private bool axisInUse = false; //so only one choice can be selected at a time

    // Use this for initialization
    void Start() {
        canWalk = true; //can walk by default
    }

    // Update is called once per frame
    void Update() {
        //DIRECT CONTROL INPUTS
        if (inputMode == InputMode.DirectControl)
        {
            //MENU BUTTON
            if (CrossPlatformInputManager.GetButtonDown("Menu"))
            {
                MenuManager.Instance.OpenMainMenu();
            }

            ProcessMovement();
        }// END DIRECT INPUT 

        if (inputMode == InputMode.DialogueSelect) { ProcessDialogueSelect(); }
        if (inputMode == InputMode.CombatTarget) { ProcessCombatTarget(); }

		//ACTION BUTTON
		if (Input.GetKeyDown(KeyCode.Return) || CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			if (notifyActionButtonObservers != null) { notifyActionButtonObservers(); }
		}

		//CANCEL BUTTON
		if(CrossPlatformInputManager.GetButtonDown("Cancel")){
			if(notifyCancelButtonObservers != null){ notifyCancelButtonObservers(); }
		}

        _v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        _h = CrossPlatformInputManager.GetAxisRaw("Horizontal");   
    }

    //INPUT STATE MANAGEMENT
    //TODO: this state management might be overkill here so let's reveevaluate after I complete combat
    public void InputModeDirect() {
        inputMode = InputMode.DirectControl;
    }

    public void InputModeDialogue() {
        inputMode = InputMode.DialogueSelect;
    }

    public void InputModeMenu() {
        inputMode = InputMode.MenuSelect;
    }

    public void InputModeTarget() {
        cm = FindObjectOfType<CombatManager>();
        inputMode = InputMode.CombatTarget;
    }

    public void EnableMovement() {
        canWalk = true;
    }

    public void DisableMovement() {
        canWalk = false;
    }

    void ProcessMovement() {
        //Random combat generator needs to know if player is walking
        if (_h != 0 || _v != 0) { isWalking = true; }
        else { isWalking = false; }
    }

    void ProcessDialogueSelect() {

        if (_v == 1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                DialogueManager.active.NavigateUp();
            }

        }

        if (_v == -1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                DialogueManager.active.NavigateDown();
            }
        }

        if (_v == 0)
        {
            axisInUse = false;
        }
    }

    void ProcessCombatTarget() {
        if (_h == 1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                cm.TargetRight();
            }
        }

        if (_h == -1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                cm.TargetLeft();
            }
        }

        if (_h == 0)
        {
            axisInUse = false;
        }
    }

}
