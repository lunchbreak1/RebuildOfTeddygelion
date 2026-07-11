using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private bool selected;

    [SerializeField] private UnityEvent optionActions;
    [SerializeField] private GameObject cursor;


    private void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

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

        if (textMesh != null)
        {
            textMesh.color = Color.yellow;
        }
        
        cursor.SetActive(true);
    }

    public void DeselectOption()
    {
        selected = true;

        if (textMesh != null) {
            textMesh.color = Color.white;
        }
        
        cursor.SetActive(false);
    }
}