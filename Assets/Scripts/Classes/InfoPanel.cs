using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public List<OptionsLayout> otherMenus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
