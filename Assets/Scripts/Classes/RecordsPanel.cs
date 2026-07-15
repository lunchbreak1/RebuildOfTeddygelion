using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsPanel : MonoBehaviour
{
    public SaveDataRecord recordPrefab;

    public GameObject recordsContainer;

    public List<OptionsLayout> otherMenus;

    public RecordPagination pagination;

    List<SaveData> saves;

    List<SaveDataRecord> records;
    // Start is called before the first frame update
    void Awake()
    {
        PopulateRecords();
    }

    public void PopulateRecords()
    {
        if (saves != null)
        {
            saves.Clear();
        }

        if (records != null)
        {
            foreach (SaveDataRecord rec in records)
            {
                Debug.Log("Clearing record: " + rec.nameText.text);
                Destroy(rec.gameObject);
                pagination.records.Clear();
            }
        }

        saves = SaveManager.LoadAll();

        records = new List<SaveDataRecord>();

        foreach (SaveData save in saves)
        {
            SaveDataRecord record = Instantiate(recordPrefab);
            record.Create(save);
            record.transform.SetParent(recordsContainer.transform, false);
            records.Add(record);
        }

        pagination.PaginateRecords(records);
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
