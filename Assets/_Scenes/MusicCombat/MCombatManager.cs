using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCombatManager : MonoBehaviour {
	public static MCombatManager mcom;

	public Text scoreDisplay;
	public int score;

	public bool isSpawning = true;

	public AudioClip clip;

	public bool canScore;

	public GameObject sphere;

	public float barTime;
	public float bpm;


	private void Awake() {
		 if (mcom != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            mcom = this;
        }

        DontDestroyOnLoad(mcom.gameObject);
	}

	// Use this for initialization
	void Start () {

		bpm = 120f;

		//finds the notes per second assuming 4 notes per bar
		barTime = (60.0f/bpm) * 4;

		StartCoroutine(SpawnSpheres());
	}

	IEnumerator SpawnSpheres(){

		while(isSpawning){
			Instantiate(sphere);
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(canScore && Input.GetKeyDown(KeyCode.Space)){
			score += 10;
			this.GetComponent<AudioSource>().clip = clip;
			this.GetComponent<AudioSource>().Play();

		}

		if(Input.GetKeyDown(KeyCode.RightAlt)){
			this.GetComponent<AudioSource>().clip = clip;
			this.GetComponent<AudioSource>().Play();
		}
		
	}

	private void LateUpdate() {
		scoreDisplay.text = score.ToString();
	}
}
