using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        if (playingViolin && bowOn  && movingArm)
        {
            if (aud.isPlaying == false)
            {
                aud.Play();
            }
        }
        else
        {
            if (aud.isPlaying == true)
            {
                aud.Pause();
            }
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
