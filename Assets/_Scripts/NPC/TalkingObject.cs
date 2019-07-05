using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: extract a lot of this logic into a proper InkManager
public class TalkingObject : MonoBehaviour
{

    [SerializeField]
    string sceneToRead = "default";

    bool playerInRange;
    private PlayerControl _player;

    private void OnEnable() {
        InputManager.im.notifyActionButtonObservers += ActionButtonEvent;
    }

    private void OnDisable() {
        InputManager.im.notifyActionButtonObservers -= ActionButtonEvent;
    }

    /* 
	Handles checking if player is close enough to talk to object
	this will become confusing if the talkable object trigger collider overlaps
	with another talkingobject

	TODO: add error warnings if trigger overlaps with another talking objects
	*/
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            _player = other.gameObject.GetComponent<PlayerControl>();
            _player.SpawnBang();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            _player.DestroyBang();
        }
    }

    //TODO: I hate these long conditionals... this needs a rewrite
    public void ActionButtonEvent() {
        if (playerInRange && InputManager.im.inputMode == InputMode.DirectControl)
        {
            _player.DestroyBang();
            InkManager.active.ReadScene(this.sceneToRead);
        }
    }
}
