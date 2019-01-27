﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; //TODO: extract this to be only in player controller

public class RandomCombatGenerator : MonoBehaviour
{

    //TODO: maybe array of scenes to load for combat 
    // for different backgrounds 

    public bool combatActive = false;
    public int stepsUntilCombat; //higher number makes combat less frequent

    //init at 5 to avoid accidental trigger
    private int _combatTrigger = 5;
    private int _stepsTaken = 0;

    bool isWalking;

    // Use this for initialization
    void Start()
    {
        if (combatActive) { StartCoroutine(CheckCombat()); }
    }

    public void LoadCombat()
    {
        GameManager.gm.LoadCombat();
    }

    IEnumerator CheckCombat()
    {
        Debug.Log("check combat has begun");
        //TODO: make a delegate or public bool on player controler = isWalking?
        // while(player.isWalking)
        while (combatActive)
        {
            _combatTrigger = Random.Range(0, stepsUntilCombat);

            Debug.Log("combat trigger = " + _combatTrigger);

            if (_combatTrigger < _stepsTaken)
            {
                combatActive = false;
                LoadCombat();
            }
            else
            {
                _stepsTaken++;
                yield return new WaitForSeconds(0.7f); //TODO: maybe find a way of measuring steps better
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        if (h != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}