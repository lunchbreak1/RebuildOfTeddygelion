using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] List<KeyboardKey> keys = new List<KeyboardKey>();
    [SerializeField] int columns = 12;

    private int currentIndex = 0;

    private bool verticalPressed = false;

    private bool horizontalPressed = false;

    private bool submitPressed = true;

    private bool cancelPressed = true;

    [SerializeField] GameObject savePanel, playAgainPanel;

    [SerializeField] TMP_InputField inputField; 
    private void OnEnable()
    {
        SelectOption(currentIndex);
    }
    // Update is called once per frame
    void Update()
    {
        GetHorizontalInput();

        GetVerticalInput();

        GetSubmitInput();

        GetCancelInput();
    }

    void GetHorizontalInput()
    {
        if (keys.Count == 0)
            return;

        float input = Input.GetAxisRaw("Horizontal");

        if (!horizontalPressed)
        {
            if (input > 0.5f)
            {
                currentIndex = (currentIndex + 1) % keys.Count;
                ChangeIndex(currentIndex);
                horizontalPressed = true;
            }
            else if (input < -0.5f) 
            {
                currentIndex = (currentIndex - 1 + keys.Count) % keys.Count;
                ChangeIndex(currentIndex);
                horizontalPressed = true;
            }
        }

        if (Mathf.Abs(input) < 0.2f)
        {
            horizontalPressed = false;
        }
    }

    void GetVerticalInput()
    {
        if (keys.Count == 0)
            return;

        float input = Input.GetAxisRaw("Vertical");

        if (!verticalPressed)
        {
            if (input < -0.5f)
            {
                currentIndex = (currentIndex + columns) % keys.Count;
                ChangeIndex(currentIndex);
                verticalPressed = true;
            }
            else if (input > 0.5f)
            {
                currentIndex = (currentIndex - columns + keys.Count) % keys.Count;
                ChangeIndex(currentIndex);
                verticalPressed = true;
            }
        }

        if (Mathf.Abs(input) < 0.2f)
        {
            verticalPressed = false;
        }
    }

    void GetSubmitInput()
    {
        float input = Input.GetAxisRaw("Submit");

        // Wait until Submit is released
        if (submitPressed)
        {
            if (Mathf.Abs(input) < 0.2f)
            {
                submitPressed = false;
            }

            return;
        }

        // Submit can now be pressed again
        if (input > 0.5f)
        {
            keys[currentIndex].PerformAction();
            submitPressed = true;
        }
    }

    void GetCancelInput()
    {
        float input = Input.GetAxisRaw("Cancel");

        if (cancelPressed)
        {
            if (Mathf.Abs(input) < 0.2f)
            {
                cancelPressed = false;
            }

            return;
        }

        // Submit can now be pressed again
        if (input > 0.5f)
        {
            Backspace();
            cancelPressed = true;
        }
    }

    public void ChangeIndex(int index)
    {
        DeselectAllOptions();
        SelectOption(index);
    }

    void DeselectAllOptions()
    {
        foreach (KeyboardKey key in keys)
        {
            key.DeselectOption();
        }
    }

    void SelectOption(int index)
    {
        keys[currentIndex].SelectOption();
    }

    public void Backspace()
    {
        inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
    }

    public void Cancel()
    {
        playAgainPanel.SetActive(true);
        savePanel.SetActive(false);
    }
}
