using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToAdd;
    [SerializeField] TMP_InputField entryField;
    [SerializeField] private Outline outline;
    [SerializeField] private UnityEvent actions;

    public void AddText()
    {
        entryField.text += textToAdd.text;
    }

    public void SetGlow(bool enabled)
    {
        outline.enabled = enabled;
    }

    public void ToggleGlow()
    {
        outline.enabled = !outline.enabled;
    }

    public void PerformAction()
    {
        actions?.Invoke();
    }

    public void Backspace()
    {
        if(entryField.text.Length > 0)
        {
            entryField.text = entryField.text.Substring(0, entryField.text.Length - 1);
        }
    }

    public void SelectOption()
    {
        SetGlow(true);
    }

    public void DeselectOption()
    {
        SetGlow(false);
    }
}
