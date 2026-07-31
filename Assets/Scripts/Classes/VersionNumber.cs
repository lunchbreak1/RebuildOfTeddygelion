using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionNumber : MonoBehaviour
{
    TextMeshProUGUI versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText = GetComponent<TextMeshProUGUI>();
        versionText.text = $"<color=yellow>Version: {Application.version}</color>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
