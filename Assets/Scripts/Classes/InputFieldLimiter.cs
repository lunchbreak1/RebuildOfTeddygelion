using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldLimiter : MonoBehaviour
{
    [SerializeField] private int characterLimit = 16;

    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.characterLimit = characterLimit;
        inputField.onValueChanged.AddListener(EnforceLimit);
    }

    private void EnforceLimit(string text)
    {
        if (text.Length > characterLimit)
        {
            inputField.text = text.Substring(0, characterLimit);
        }
    }

    private void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(EnforceLimit);
        }
    }
}