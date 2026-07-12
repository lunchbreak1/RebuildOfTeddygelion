using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsPanel : MonoBehaviour
{
    public SaveDataRecord recordPrefab;

    public GameObject recordsContainer;

    public List<OptionsLayout> otherMenus;

    List<SaveData> saves;
    // Start is called before the first frame update
    void Start()
    {
        PopulateRecords();
    }

    public void PopulateRecords()
    {
        if (saves != null)
        {
            saves.Clear();

            SaveDataRecord []  recordsToDelete = recordsContainer.GetComponentsInChildren<SaveDataRecord>();

            foreach (SaveDataRecord rec in recordsToDelete)
            {
                Debug.Log("Clearing record: " + rec.nameText.text);
                Destroy(rec.gameObject);
            }
        }

        saves = SaveManager.LoadAll();

        foreach (SaveData save in saves)
        {
            SaveDataRecord record = Instantiate(recordPrefab);
            record.Create(save);
            record.transform.SetParent(recordsContainer.transform, false);
        }
    }

    private void Update()
    {
        if(Input.GetAxis("Cancel") > 0.5f)
        {
            gameObject.SetActive(false);

            foreach(OptionsLayout layout in otherMenus)
            {
                layout.enabled = true;
                layout.ChangeIndex(0);
            }
        }
    }
}
