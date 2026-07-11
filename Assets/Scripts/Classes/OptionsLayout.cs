using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLayout : MonoBehaviour
{
    [SerializeField] private List<Option> options = new List<Option>();
    [SerializeField] string axis;
    private int currentIndex = 0;

    private bool axisPressed;

    private void Start()
    {
        ChangeIndex(0);
    }

    void Update()
    {
        float input = Input.GetAxisRaw(axis);

        if (!axisPressed)
        {
            if (input > 0.5f)
            {
                currentIndex = (currentIndex + 1) % options.Count;
                ChangeIndex(currentIndex);
                axisPressed = true;
            }
            else if (input < -0.5f)
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

        if(Input.GetAxis("Submit") > 0)
        {
            options[currentIndex].PerformAction();
        }
    }

    void ChangeIndex(int index)
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
