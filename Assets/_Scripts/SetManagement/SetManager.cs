using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
	public List<Set> sets;

	public bool handleInit;

	private GameObject activeSet;
	//SINGLETON
    public static SetManager active;
    private void Awake() {
        if (active != null) { Destroy(this.gameObject); }
        else { active = this; }
    }

	private void Start() {
		//TODO: The initial set should be called from ink or GameManager set should be call
		if(handleInit)
			LoadSet("Home");
	}


	public void LoadSet(string setName){
		if(activeSet != null){
			Destroy(activeSet);
		}

		Set set = findSetByName(setName); 
		activeSet = Instantiate(set.setPrefab);

	}

	private Set findSetByName(string name){
		return sets.Find(x => x.name.Contains(name));
	}

}
