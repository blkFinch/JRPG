using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //for list handling


public class DialogueManager : MonoBehaviour {

	public GameObject textBox;
	public Button choiceButtonPrefab;
	public Canvas dialogueCanvas;
	public float textPrintSpeed = 0.1f;

	private GameObject activeTextBox;
	private List<Button> activeChoiceButtons;
	private int activeChoiceIndex;

	public bool isDisplayingBox;
	private string activeText;
	private List<string> activeChoices;
	private bool canChoose = false;

	/*
	 	For now I'm making a listener delegate pattern for making choices so
		 DialogueManager is ambivalent to the presence of talking objects 
		 this may cause confusion for other TalkingObjects in scene but 
		 we'll see... -finch
	 */
	public delegate void OnDialogueChoice(int index);
	public event OnDialogueChoice NotifyDialogueChoiceListeners;

	//SINGLETON
	public static DialogueManager active;

  private void Awake() {
		if( active != null){ GameObject.Destroy(this);}
		else{active = this;}

		DontDestroyOnLoad(active.gameObject);
	}

	// Use this for initialization
	void Start () {
		isDisplayingBox = false;
		activeChoiceButtons = new List<Button>();

		InputManager.im.notifyActionButtonObservers += SelectActiveChoice;
	}

	//RECIEVE AND RENDER 
	
	public void RecieveInkData(string text, List<string> choices = null ){
		
		//TODO: Find a way to clear previous data before spawning box

		//Assigns data
		this.activeText = text;
		this.activeChoices = choices; 

		//Display data
		DisplayText();
		if(this.activeChoices != null){DisplayChoices();}

		//Realign Data To UI
		StartCoroutine(TouchLayout()); //TODO: find better solution	

	}

	public void PlayerDialogueChoice(int index){
		Debug.Log("notifying listeners! choice index: " + index);

		ClearDialogueCanvas();

		NotifyDialogueChoiceListeners(index); 
	}

	/*
		This toggles a child control width on the vertical layout group after the frame
		for some reason this causes the layout group to realign properly. It seems as though
		the objects placed into the layout need to be realigned the frame after they are instantiated

		--there may be a better solution
		TODO: Create UILayoutManager.cs to handle all layout rendering
	*/
	private IEnumerator TouchLayout(){
		yield return new WaitForEndOfFrame();
		dialogueCanvas.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
		dialogueCanvas.GetComponent<VerticalLayoutGroup>().childControlWidth = false;

		canChoose = true;
	}

	//DISPLAY TEXT TO SCREEN

	void DisplayText(){
		
		InputManager.im.InputModeDialogue();

		if(!isDisplayingBox){
			isDisplayingBox = true; //TODO: do I still need this bool?
			activeTextBox = Instantiate(textBox);
			activeTextBox.transform.SetParent(dialogueCanvas.transform, false);
			
			activeTextBox.GetComponentInChildren<Text>().text = activeText;
		}
		
	}

	void DisplayChoices(){

		for(int i = 0; i < activeChoices.Count; i ++){
			Button button = Instantiate(choiceButtonPrefab);
			button.transform.SetParent(dialogueCanvas.transform, false);
			button.GetComponentInChildren<Text>().text = activeChoices[i];


			int _buttonIndex = i; //captures the int to create a unique instance of the event listener
			button.onClick.AddListener(() => PlayerDialogueChoice(_buttonIndex));
			
			activeChoiceButtons.Add(button); 
		}

		ActiveChoice(0);
	}

	//Tthis doesnt look as good as I'd hoped for now it is deprecated and unused
	private IEnumerator PrintTextToBox(){
		char[] _chars = this.activeText.ToCharArray();
		string _printedString = "";
		int _i = 0;

		while(_i < _chars.Length){
			_printedString += _chars[_i];
			activeTextBox.GetComponentInChildren<Text>().text = _printedString;
			_i ++;
			yield return new WaitForSeconds(textPrintSpeed);
		}

		if(this.activeChoices != null){DisplayChoices();}

		//Realign Data To UI
		StartCoroutine(TouchLayout()); //TODO: find better solution	
	}

	//NAVIGATE CHOICES

	//TODO: clean this for readability
	void ActiveChoice(int selectedButtonIndex){
		Button selectedButton = activeChoiceButtons[selectedButtonIndex];

		for(int i = 0; i < activeChoiceButtons.Count; i++){
			activeChoiceButtons[i].image.color = Color.blue;
		}

		selectedButton.image.color = new Color(0,1,0);
		activeChoiceIndex = selectedButtonIndex;
	}

	public void NavigateUp(){
		if( activeChoiceIndex > 0){
			ActiveChoice(activeChoiceIndex - 1);
		}
	}

	
	public void NavigateDown(){
		if( activeChoiceIndex < activeChoiceButtons.Count - 1 ){
			ActiveChoice(activeChoiceIndex + 1);
		}
	}

	public void SelectActiveChoice(){
		if(InputManager.im.inputMode == InputMode.DialogueSelect && activeChoices != null){
			if(canChoose){PlayerDialogueChoice(activeChoiceIndex);	}
		}
	}

	//CLEAR AND EXIT
	void ClearDialogueCanvas(){
		if(isDisplayingBox){ DestroyTextBox(); }
	}

	//Destroys TextBox and all associated choice boxes restting to default
	public void DestroyTextBox(){
		if(isDisplayingBox){

			Destroy(activeTextBox.gameObject);
			isDisplayingBox = false;

			//destroys all choice buttons and removes them from list
			if(activeChoiceButtons.Any()){
				foreach(var button in activeChoiceButtons){
					Destroy(button.gameObject);
				}

				activeChoiceButtons.Clear();
				activeChoices.Clear();
				
			}
		}
	}

	public void ExitDialogue(){
		DestroyTextBox();

		if(InputManager.im){InputManager.im.InputModeDirect();}
	}

}
