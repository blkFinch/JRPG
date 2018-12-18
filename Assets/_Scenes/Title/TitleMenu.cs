using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TitleMenu : MonoBehaviour {

	public Button _newGameButton, _loadGameButton, _submitNewHeroButton, _startButton;
	public Canvas titleCanvas, newGameCanvas;
	public InputField nameField;
	private GameManager gameManager;

	void Awake() {
		gameManager = FindObjectOfType<GameManager>();
		_newGameButton.onClick.AddListener(StartNewGame);
		_loadGameButton.onClick.AddListener(LoadGame);
		_submitNewHeroButton.onClick.AddListener(NewHero);
		_startButton.onClick.AddListener(SaveNewGame);

		if(File.Exists(Application.persistentDataPath + "/heroData.gd")){
			_loadGameButton.gameObject.SetActive(true);
		}else
		{
			_loadGameButton.gameObject.SetActive(false);
		}

	}

	void StartNewGame(){
		Debug.Log("New Game started!");
		titleCanvas.gameObject.SetActive(false);
		newGameCanvas.gameObject.SetActive(true);
	}

	void NewHero(){
		gameManager.CreateNewHero(nameField.text);
	}

	void LoadGame(){
		SaveLoad.LoadHero();
		StartGame();
	}

	void SaveNewGame(){
		SaveLoad.SaveHero();
		StartGame();
	}

	void StartGame(){
		gameManager.LoadScene("World");
	}
}
