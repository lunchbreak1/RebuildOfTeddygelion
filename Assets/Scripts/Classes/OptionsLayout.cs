using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLayout : MonoBehaviour
{
    [SerializeField] private List<Option> options = new List<Option>();
    [SerializeField] string axis;
    private int currentIndex = 0;

    private bool axisPressed;

    private bool submitPressed;

    void Start()
    {
        //ChangeIndex(0);
    }

    void Update()
    {
        GetDirectionalInput();

        GetSubmitInput();
    }

    void GetDirectionalInput()
    {
        float input = Input.GetAxisRaw(axis);

        if (!axisPressed)
        {
            if (input < -0.5f)
            //if (input > 0.5f)
            {
                currentIndex = (currentIndex + 1) % options.Count;
                ChangeIndex(currentIndex);
                axisPressed = true;
            }
            else if (input > 0.5f)
            // else if (input < -0.5f)
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

        if (!submitPressed && input > 0.5f)
        {
            options[currentIndex].PerformAction();
        }

        if (Mathf.Abs(input) < 0.2f)
        {
            submitPressed = false;
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
