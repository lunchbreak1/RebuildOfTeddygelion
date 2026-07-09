using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsPanel : MonoBehaviour
{
    public SaveDataRecord recordPrefab;

    public GameObject recordsContainer;

    List<SaveData> saves;
    // Start is called before the first frame update
    void Start()
    {
        saves = SaveManager.LoadAll();

        foreach (SaveData save in saves)
        {
            Debug.Log($"{save.playerName}: {save.level}: {save.score}");
        }

        PopulateRecords();
    }

    public void PopulateRecords()
    {
        foreach (SaveData save in saves)
        {
            SaveDataRecord record = Instantiate(recordPrefab);
            record.Create(save);
            record.transform.SetParent(recordsContainer.transform, false);
        }
    }
}
