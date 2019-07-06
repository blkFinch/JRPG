using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq; //for list handling

/*
	InkManager handles all the reading of ink
	this is the only place ink should be evaluated
	only strings come in or out
 */

public class InkManager : MonoBehaviour
{
    public TextAsset inkScript;
    public Story story;

    //story text is the active string from the story
    string storyText;
    public string StoryText {
        get { return storyText; }
    }
    List<Choice> activeChoiceList = new List<Choice>();


    public static InkManager active;

    private void Awake() {
        active = this;
    }

    // INIT/RESET METHODS
    //
    void Start() {
        
        InitInk();
    }

    public void InitInk() {
        initScene();
        initExternalMethods();
    }

    private void initScene() {
        story = new Story(inkScript.text);
    }

    private void initExternalMethods() {
        //checks if hero has a given item
        story.BindExternalFunction("check_item", (int id) =>
      {
          return Hero.active.inventory.HasItem(id);
      });

        //returns name
        story.BindExternalFunction("hero_name", () =>
       {
           return Hero.active.data.Name;
       });
    }

    // READ / PROCESS INK
    //
    public void ReadScene(string scene) {

        story.ChoosePathString(scene);
        ReadInk(); //loads ink story while it can continue
        SendTextToDialogueManager(); //sends the story as a string to be displayed
    }


    public void ReadInk() {
        //Catch a broken story link
        if (!story.canContinue) { DialogueManager.active.ExitDialogue(); }

        while (story.canContinue)
        {
            this.storyText = story.Continue();
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                activeChoiceList.Add(choice);
            }
        }

        checkTags();
    }

    public void ChooseChoice(int index) {
        StartCoroutine(LateChoice(index));
    }

    private IEnumerator LateChoice(int index){
        yield return new WaitForEndOfFrame();
        story.ChooseChoiceIndex(index);
        ReadInk();
        SendTextToDialogueManager();
    }

    /*
      This sends all the text and choices from the current 'step' in the story
      to the Dialogue Manager to be displayed
   */
    void SendTextToDialogueManager() {
        List<string> packagedChoices = new List<string>();
        foreach (var choice in activeChoiceList)
        {
            packagedChoices.Add(choice.text);
        }

        if (packagedChoices.Any()) { DialogueManager.active.RecieveInkData(storyText, packagedChoices); }
        else { DialogueManager.active.RecieveInkData(storyText); }

        //empty the choice lists to avoid double sending
        activeChoiceList.Clear();
    }


    //INK EXTERNALS
    //
    /*
        These external methods have been refactored out here so
        they are not called by accident while the ink is compiled
     */
    private void checkTags() {
        if (story.currentTags.Contains("ADD_ITEM"))
        {
            addItemFromInk();
        }

        if (story.currentTags.Contains("LOAD_SCENE"))
        {
            changeSceneFromInk();
        }
    }

    private void addItemFromInk() {
        int id = (int)story.variablesState["item_to_add"];
        Hero.active.inventory.AddItemFromID(id);
    }

    private void changeSceneFromInk() {
        string sceneToLoad = (string)story.variablesState["scene_to_load"];
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            GameManager.gm.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Invalid scene, Please check value of ink variable 'scene_to_load'");
        }
    }
}
