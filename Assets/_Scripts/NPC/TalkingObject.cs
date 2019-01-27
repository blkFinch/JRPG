using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq; //for list handling

public class TalkingObject : MonoBehaviour
{

    [SerializeField]
    string sceneToRead;

    public TextAsset inkScript;
    Story _inkStory;

    bool playerInRange;

    //these are for rendering the scene out 
    string storyText;
    public string StoryText
    {
        get
        {
            return storyText;
        }
    }

    List<Choice> activeChoiceList = new List<Choice>();

    GameObject player;
    PlayerControl tps;

    private void Awake()
    {
        _inkStory = new Story(inkScript.text);
    }

    private void Start()
    {
        InputManager.im.notifyActionButtonObservers += ActionButtonEvent;
        DialogueManager.active.NotifyDialogueChoiceListeners += ChooseChoice;
    }

	//Handles parsing the ink json asset
    public void SetSceneToRead(string scene){
        this.sceneToRead = scene;
    }

    public void ReadInk()
    {
        

        while (_inkStory.canContinue)
        {
            this.storyText = _inkStory.Continue();
        }

        if (_inkStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _inkStory.currentChoices.Count; i++)
            {
                Choice choice = _inkStory.currentChoices[i];
                activeChoiceList.Add(choice);
            }
        }
    }

    public void ChooseChoice(int index){
        _inkStory.ChooseChoiceIndex(index);
        ReadInk();
        SendTextToDialogueManager();
    }

	/* 
	Handles checking if player is close enough to talk to object
	this will become confusing if the talkable object trigger collider overlaps
	with another talkingobject

	TODO: add error warnings if trigger overlaps with another talking objects
	*/ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
			//TODO: spawn exclamation point above talkable object
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            DialogueManager.active.DestroyTextBox();
        }
    }

    public void ActionButtonEvent()
    {
        if (playerInRange && !DialogueManager.active.isDisplayingBox) 
        {
            _inkStory.ChoosePathString(this.sceneToRead); 
            
            ReadInk(); //loads ink story while it can continue
            SendTextToDialogueManager(); //sends the story as a string to be displayed
        }
    }

	/*
		This sends all the text and choices from the current 'step' in the story
		to the Dialogue Manager to be displayed

	 */
    void SendTextToDialogueManager()
    {
        //TODO: consider extracting the choice packaging logic out
        List<string> packagedChoices = new List<string>();
        foreach (var choice in activeChoiceList)
        {
            packagedChoices.Add(choice.text);
        }

        if(packagedChoices.Any()){ 
			DialogueManager.active.RecieveInkData(storyText, packagedChoices);

            //empty the choice lists to avoid double sending
            activeChoiceList.Clear();
		}
		else{ DialogueManager.active.RecieveInkData(storyText); }
    }

}
