using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // Canvas and Panels --- 
    //TODO: hold stats and windows in different panels 
    // and enable them with setActive as player selects through menu
    public Canvas menuCanvas;

    //Buttons
    public Button exitButton;


    bool toggleActive = false;

    // Use this for initialization
    void Start()
    {
        menuCanvas.gameObject.SetActive(false);

        //set listeners
        exitButton.onClick.AddListener(ToggleMenu);

    }

    void ToggleMenu()
    {
        toggleActive = !toggleActive;
        menuCanvas.gameObject.SetActive(toggleActive);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: convert to cross platform / controller 
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }
}
