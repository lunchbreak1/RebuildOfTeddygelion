using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public List<OptionsLayout> otherMenus;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetAxis("Cancel") > 0.5f)
        {
            gameObject.SetActive(false);

            foreach (OptionsLayout layout in otherMenus)
            {
                layout.enabled = true;
                layout.ChangeIndex(0);
            }
        }
    }
}
