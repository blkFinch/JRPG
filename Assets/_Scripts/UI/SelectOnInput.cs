using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start () {
		//in case you forget to set the eventSystem
		//this will probably fail if there are multiple event systems -finch
		if(!eventSystem){
			eventSystem = FindObjectOfType<EventSystem>();
		}
    }
    
    // Update is called once per frame
    void Update () 
    {
        if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
