using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMode : MonoBehaviour
{
    public ControllerScheme scheme = ControllerScheme.MouseAndKeyboard;

    public event System.Action ControllerConnected;
    public event System.Action ControllerDisconnected;

    private string[] xboxAxes =
    {
        "Xbox_Buttons",
        "Xbox_JoystickX",
        "Xbox_JoystickY",
        "TriggerRight",
        "TriggerLeft",
        "TriggerRight2",
        "TriggerLeft2"
    };

    private bool XboxInputDetected()
    {
        foreach (string axis in xboxAxes)
        {
            if (Mathf.Abs(Input.GetAxis(axis)) > 0.1f)
                return true;
        }

        return false;
    }


    private bool KeyboardInputDetected()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
          Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
      Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Space) ||
      Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) ||
      Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2);
    }

    private bool KeyboardInputNotDetected()
    {
        return !Input.GetKey(KeyCode.W) &&
               !Input.GetKey(KeyCode.A) &&
               !Input.GetKey(KeyCode.S) &&
               !Input.GetKey(KeyCode.D) &&
               !Input.GetKey(KeyCode.UpArrow) &&
               !Input.GetKey(KeyCode.LeftArrow) &&
               !Input.GetKey(KeyCode.RightArrow) &&
               !Input.GetKey(KeyCode.DownArrow) &&
               !Input.GetKey(KeyCode.Z) &&
               !Input.GetKey(KeyCode.X) &&
               !Input.GetKey(KeyCode.C) &&
               !Input.GetKey(KeyCode.Space) &&
               !Input.GetKey(KeyCode.Escape) &&
               !Input.GetKey(KeyCode.Return) &&
               !Input.GetMouseButton(0) &&
               !Input.GetMouseButton(1) &&
               !Input.GetMouseButton(2);
    }

    private bool IsControllerConnected()
    {
        string[] joysticks = Input.GetJoystickNames();

        foreach (string joystick in joysticks)
        {
            if (!string.IsNullOrEmpty(joystick))
                return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(scheme)
        {
            case ControllerScheme.MouseAndKeyboard:
                //if (XboxInputDetected() && KeyboardInputNotDetected() && (Input.anyKeyDown || Input.anyKey))
                if(IsControllerConnected())
                {
                    Debug.Log("ControlMode: XBOX CONTROLLER DETECTED");
                    scheme = ControllerScheme.XboxController;
                    ControllerConnected?.Invoke();        
                }
                break;

            case ControllerScheme.XboxController:
                //if (KeyboardInputDetected())
                if (!IsControllerConnected())
                {
                    Debug.Log("ControlMode: MOUSE AND KEYBOARD DETECTED");
                    scheme = ControllerScheme.MouseAndKeyboard;
                    ControllerDisconnected?.Invoke();
                }
            break;


        }
        
    }
}

public enum ControllerScheme
{
    MouseAndKeyboard,
    XboxController
}