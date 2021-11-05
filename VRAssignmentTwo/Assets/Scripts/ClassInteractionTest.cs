using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClassInteractionTest : MonoBehaviour
{

    [SerializeField] private ActionBasedController _actionBasedController;

    private bool selectedHasBeenPressed;

    public GameObject sphere;
    
    private void OnEnable()
    {
        _actionBasedController.selectAction.action.performed += Select_action_performed;
        _actionBasedController.rotationAction.action.performed += Rotate_action_performed;

        _actionBasedController.selectAction.action.canceled += Select_action_canceled;
    }

    private void Select_action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Selected has been delegated by " + this.gameObject.name);
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }

    private void Select_action_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        sphere.GetComponent<Renderer>().material.color = Color.white;
    }

    private void Rotate_action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Rotated has been delegated by " + this.gameObject.name);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //selectedHasBeenPressed = _actionBasedController.selectAction.action.ReadValue<bool>();
        
        /*if (selectedHasBeenPressed)
        {
            Debug.Log("Selected has been pressed by " + this.gameObject.name);
        }*/
    }
    
    private void OnDisable()
    {
        _actionBasedController.selectAction.action.performed -= Select_action_performed;
        _actionBasedController.rotationAction.action.performed -= Rotate_action_performed;
        
        _actionBasedController.selectAction.action.canceled -= Select_action_canceled;
    }
}
