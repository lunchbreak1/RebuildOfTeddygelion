using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private bool selected;

    [SerializeField] private UnityEvent optionActions;
    [SerializeField] private GameObject cursor;

    public void PerformAction()
    {
        if (selected)
        {
            optionActions?.Invoke();
        }
    }

    public void SelectOption()
    {
        selected = true;
        textMesh.color = Color.yellow;
        cursor.SetActive(true);
    }

    public void DeselectOption()
    {
        selected = true;
        textMesh.color = Color.yellow;
        cursor.SetActive(false);
    }
}