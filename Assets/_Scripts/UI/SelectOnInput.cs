using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;

    public GameObject selectedObject;

    public ArrayList buttons;
    public static SelectOnInput active;

    private bool buttonSelected;

    private void Awake() {
        if(active == null){
            active = this;
        }

        buttons = new ArrayList();
    }

    // Use this for initialization
    void Start () {
		//in case you forget to set the eventSystem
		//this will probably fail if there are multiple event systems -finch
		if(!eventSystem){
			eventSystem = FindObjectOfType<EventSystem>();
		}
    }

    public void AddButton(MenuButton _btn){
        buttons.Add(_btn);
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
        {
            Debug.Log("AXIS NOT 0");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

        foreach(var btn in buttons){
            Debug.Log(btn);
        }

    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
