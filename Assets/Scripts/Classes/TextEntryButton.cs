using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextEntryButton : MonoBehaviour
{
    TextMeshProUGUI textToAdd;
    [SerializeField] TMP_InputField entryField;
    UnityEngine.UI.Button button;
    // Start is called before the first frame update
    void Start()
    {
        textToAdd = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(AddText);
    }

    public void AddText()
    {
        entryField.text += textToAdd.text;
    }

    
}
