using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Violining : MonoBehaviour
{

    public bool bowOn = false;

    public bool playingViolin = false;
    
    private ActionBasedController _actionBasedController;

    public AudioSource aud;

    private Vector3 controllerPos;

    private bool movingArm = false;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        var actionController = GameObject.Find("RightHand Controller");
        _actionBasedController = actionController.GetComponent<ActionBasedController>();
        
        _actionBasedController.selectAction.action.performed += Select_action_performed;
        _actionBasedController.selectAction.action.canceled += Select_action_canceled;

        _actionBasedController.positionAction.action.performed += positionAction;
    }


    private void positionAction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (controllerPos == null)
        {
            controllerPos = _actionBasedController.gameObject.transform.position;
        }
        
        
        float dist = Vector3.Distance(controllerPos, _actionBasedController.gameObject.transform.position);

        if (dist > 0.0001f)
        {
            movingArm = true;
        }
        else
        {
            movingArm = false;
        }
        
        controllerPos = _actionBasedController.gameObject.transform.position;
        
    }
    
    private void Select_action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playingViolin = true;
    }

    private void Select_action_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playingViolin = false;
    }
    
    
    // Update is called once per frame
    void Update()
    {

        //This was for testing on the computer. 

        /*if (Input.GetKey(KeyCode.Space))
        {
            playingViolin = true;
            bowOn = true;
            movingArm = true;
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            playingViolin = false;
            bowOn = false;
            movingArm = false;
        }*/
        
        //Rather than turn on an off audio sources, which are on a different object
        //I'm just turning off the entire audio listener now that I have more than one source
        //So this made transitions a little easier too, since the music is 'playing' based on the button presses
        //but otherwise can't be heard if you're not 'playing'
        

        if (playingViolin && bowOn  && movingArm)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    //this does nothing right now :( couldn't get this working yet unfortunately
    void recurseHaptics()
    {
        _actionBasedController.SendHapticImpulse(0.7f, 0.1f);
        if (aud.isPlaying == true)
        {
            Invoke(nameof(recurseHaptics), 0.1f);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BowCollider")
        {
            bowOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "BowCollider")
        {
            bowOn = false; 
        }
    }
}
