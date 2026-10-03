using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    public GameObject keyboardInstructions;
    public GameObject xBoxInstructions;
    ControllerMode controllerMode;
    // Start is called before the first frame update
    void Start()
    {
        controllerMode = FindObjectOfType<ControllerMode>();

        if (controllerMode == null)
        {
            Debug.LogWarning("Can't find controller mode");
        }
        else
        {
            controllerMode.ControllerConnected += ShowXboxControls;
            controllerMode.ControllerDisconnected += ShowKeyboardControls;
        }
    }

    void ShowXboxControls()
    {
        keyboardInstructions.SetActive(false);
        xBoxInstructions.SetActive(true);
    }

    void ShowKeyboardControls()
    {
        keyboardInstructions.SetActive(true);
        xBoxInstructions.SetActive(false);
    }
}
