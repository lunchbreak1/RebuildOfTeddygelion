using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private UnityEvent optionActions;
    [SerializeField] private GameObject cursor;

    private void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void PerformAction()
    {
        optionActions?.Invoke();
    }

    public void SelectOption()
    {
        if (textMesh != null)
        {
            textMesh.color = Color.yellow;
        }
        
        cursor.SetActive(true);
    }

    public void DeselectOption()
    {
        if (textMesh != null) {
            textMesh.color = Color.white;
        }
        
        cursor.SetActive(false);
    }
}