using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosterManager : MonoBehaviour
{
    private int maxPosters = 10;
    private TextMeshProUGUI TextMeshProUGUI;
    private WheelchairController wheelchairController;
    public string xBoxInstructions;
    public string keyboardInstructions;
    private string instructions;
    private ControllerMode controllerMode;

    // Start is called before the first frame update
    void Start()
    {
        maxPosters = FindObjectsOfType<Poster>().Length;
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        wheelchairController = FindObjectOfType<WheelchairController>();
        controllerMode = FindObjectOfType<ControllerMode>();

        if (maxPosters == 0)
        {
            Debug.LogWarning("Can't find posters");
        }

        if (wheelchairController == null)
        {
            Debug.LogWarning("Can't find wheelchair");
        }

        if (TextMeshProUGUI == null)
        {
            Debug.LogWarning("Can't find textmesh");
        }

        if (controllerMode == null)
        {
            Debug.LogWarning("Can't find controller mode");
        }
        else
        {
            controllerMode.ControllerConnected += ShowXboxControls;
            controllerMode.ControllerDisconnected += ShowKeyboardControls;
        }

        instructions = keyboardInstructions;
    }

    // Update is called once per frame
    public void SetPosterMessage()
    {
        TextMeshProUGUI.text = wheelchairController.posters > 0 ? "Posters: " + wheelchairController.posters + " / " + maxPosters : instructions; 
    }

    public void ClearPosterMessage()
    {
        TextMeshProUGUI.text = "";
    }

    public void ShowXboxControls()
    {
        instructions = xBoxInstructions;
    }

    public void ShowKeyboardControls()
    {
        instructions = keyboardInstructions;
    }
}
