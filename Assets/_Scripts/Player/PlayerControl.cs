using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerControl : MonoBehaviour
{

	public GameObject Bang;
    private GameObject _bang;


    //OLD STUFF
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private Vector3 _stop = new Vector3(0, 0, 0);


    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();


    }

    private bool AcceptsPlayerInput()
    {
        //TODO: I removed canWalk check here -- if this is superfluous, remove it entirely later
        if (InputManager.im.inputMode == InputMode.DirectControl)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnBang()
    {
        _bang = Instantiate(Bang);
        MoveBang();
    }

	private void MoveBang(){
		Vector3 _bangPos = new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z);
		_bang.transform.position = _bangPos;
	}

	public void DestroyBang(){
		Destroy(_bang.gameObject);
	}

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (AcceptsPlayerInput())
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
        }
        else
        {
            m_Move = _stop;
        }
        // pass all parameters to the character control script
        m_Character.Move(m_Move, false, false); //crouch and jump are both false until I write my own movement script - finch

		//Updates the exclamation point position
		if(_bang){MoveBang();}

        if (m_Move.magnitude > 0)
        {

        }
    }

}
