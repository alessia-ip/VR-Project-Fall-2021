using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ViolinStrings : MonoBehaviour
{
    public InputActionAsset _actionBasedController;

    public AudioSource gString;
    public AudioSource dString;
    public AudioSource aString;
    public AudioSource eString;

    public GameObject gStringIndicator;
    public GameObject dStringIndicator;
    public GameObject aStringIndicator;
    public GameObject eStringIndicator;

    public AudioClip gClip;
    public AudioClip dClip;
    public AudioClip aClip;
    public AudioClip eClip;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _actionBasedController.Enable();
        
        /*G - trigger
        D - y 
        A - x
        E - grip*/
        
        //rather than reference the in scene controllers I'm just getting the general actions
        //right from the input prefab
        
        //this works the same as _actionBasedController.selectAction.action.performed
        //but with some of the inputs that don't auto-show like 'OculusX' - which is the x key
        
        _actionBasedController["OculusX"].performed += A_String_Activate;
        _actionBasedController["OculusY"].performed += D_String_Activate;
        _actionBasedController["OculusGrip"].performed += E_String_Activate;
        _actionBasedController["OculusTrigger"].performed += G_String_Activate;
        
        _actionBasedController["OculusX"].canceled += A_String_Deactivate;
        _actionBasedController["OculusY"].canceled += D_String_Deactivate;
        _actionBasedController["OculusGrip"].canceled += E_String_Deactivate;
        _actionBasedController["OculusTrigger"].canceled += G_String_Deactivate;
        
    }
    
    private void G_String_Activate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        gString.clip = gClip;
        gString.Play();
        gStringIndicator.GetComponent<MeshRenderer>().enabled = true;
    }
    
    private void D_String_Activate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       dString.clip = dClip;
       dString.Play();
       dStringIndicator.GetComponent<MeshRenderer>().enabled = true;
    }
    
    private void A_String_Activate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        aString.clip = aClip;
        aString.Play();
        aStringIndicator.GetComponent<MeshRenderer>().enabled = true;
    }
    
    private void E_String_Activate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        eString.clip = eClip;
        eString.Play();
        eStringIndicator.GetComponent<MeshRenderer>().enabled = true;
    }
    
    private void G_String_Deactivate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        gString.clip = null;
        gStringIndicator.GetComponent<MeshRenderer>().enabled = false;
    }
    
    private void D_String_Deactivate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       dString.clip = null;
       dStringIndicator.GetComponent<MeshRenderer>().enabled = false;

    }
    
    private void A_String_Deactivate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        aString.clip = null;
        aStringIndicator.GetComponent<MeshRenderer>().enabled = false;

    }
    
    private void E_String_Deactivate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        eString.clip = null;
        eStringIndicator.GetComponent<MeshRenderer>().enabled = false;

    }
    
}
