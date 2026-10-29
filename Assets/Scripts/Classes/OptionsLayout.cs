using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLayout : MonoBehaviour
{
    [SerializeField] private List<Option> options = new List<Option>();
    [SerializeField] string axis;
    private int currentIndex = 0;

    private bool axisPressed;

    private bool submitPressed = true;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        submitPressed = true;
        SelectOption(currentIndex);
    }

    private void OnDisable()
    {

    }

    void Update()
    {
        GetDirectionalInput();

        GetSubmitInput();
    }

    void GetDirectionalInput()
    {
        if (options.Count == 0)
            return;

        float input = (axis == "Horizontal") ? -Input.GetAxisRaw(axis) : Input.GetAxisRaw(axis);

        if (!axisPressed)
        {
            if (input < -0.5f)
            {
                currentIndex = (currentIndex + 1) % options.Count;
                ChangeIndex(currentIndex);
                axisPressed = true;
            }
            else if (input > 0.5f)
            {
                currentIndex = (currentIndex - 1 + options.Count) % options.Count;
                ChangeIndex(currentIndex);
                axisPressed = true;
            }
        }

        if (Mathf.Abs(input) < 0.2f)
        {
            axisPressed = false;
        }
    }

    void GetSubmitInput()
    {
        float input = Input.GetAxisRaw("Submit");

        if (submitPressed)
        {
            if (Mathf.Abs(input) < 0.2f)
            {
                submitPressed = false;
            }

            return;
        }

        if (input > 0.5f)
        {
            options[currentIndex].PerformAction();
            submitPressed = true;
        }
    }

    public void ChangeIndex(int index)
    {
        DeselectAllOptions();
        SelectOption(index);
    }    

    void DeselectAllOptions()
    {
        foreach (Option option in options)
        {
            option.DeselectOption();
        }
    }    

    void SelectOption(int index)
    {
        options[currentIndex].SelectOption();
    }
}
