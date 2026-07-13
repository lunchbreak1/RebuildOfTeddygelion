using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    public TextMeshProUGUI giveNameLabel, recordSaved;

    TrickManager trickManager;

    public float messageDuration;

    public GameObject savePanel, playAgainPanel;

    public RecordsPanel RecordsPanel;

    List<SaveData> saves;

    // Start is called before the first frame update
    void Start()
    {
        trickManager = FindAnyObjectByType<TrickManager>();

        if (trickManager == null) {
            Debug.LogError("Could not find TrickManager in SaveMenu");
        }

        saves = SaveManager.LoadAll();
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(playerName.text))
        {
            giveNameLabel.text = "<color=red><shake>Please give your file a name.</color></shake>";
            CancelInvoke("Revert");
            Invoke("Revert", messageDuration);
        }
        else
        {
            Debug.Log("Here's your name: " + playerName.text);
            SaveData data = new SaveData(playerName.text, trickManager.score, SceneManager.GetActiveScene().name);
            saves.Add(data);
            SaveManager.SaveAll(saves);
            recordSaved.text = "<color=green><shake>File saved!</color></shake>";
            savePanel.SetActive(false);
            playAgainPanel.SetActive(true);
            RecordsPanel.PopulateRecords();
        }  
    }

    public void Revert()
    {
        giveNameLabel.text = "Please give your file a name.";
    }

    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
