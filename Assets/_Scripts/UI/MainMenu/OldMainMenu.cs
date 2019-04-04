using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldMainMenu : MonoBehaviour
{

    public Canvas menuCanvas;

    //Buttons
    public Button exitButton;

    InventoryButton ib;
    MenuSaveButton sb;
    MenuLoadButton lb;
    MenuStatsButton statsb;


    bool toggleActive = false;

    PlayerControl pc;


    // Use this for initialization
    void Start()
    {
        //loading all the buttons to listen for tab changes
        ib = FindObjectOfType<InventoryButton>();
        sb = FindObjectOfType<MenuSaveButton>();
        lb = FindObjectOfType<MenuLoadButton>();
        statsb = FindObjectOfType<MenuStatsButton>();

        sb.notifyMenuTabObservers += OnTabChange;
        statsb.notifyStatsTabObservers += OnTabChange;

        menuCanvas.gameObject.SetActive(false);

        //set listeners
        exitButton.onClick.AddListener(ToggleMenu);

        InputManager.im.notifyMenuButtonObservers += ToggleMenu;

    }

    void ToggleMenu()
    {
        toggleActive = !toggleActive;
        menuCanvas.gameObject.SetActive(toggleActive);
        TogglePause(toggleActive);
    }

    private void TogglePause(bool isPaused){
        if(isPaused){ PauseGame();}
        else{ ContinueGame();}
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1;
        //enable the scripts again
    }

    public void DespawnInventoryButtons(){
        ib.ClearItemButtons();
    }

    void OnTabChange(){
        DespawnInventoryButtons();
    }

}
