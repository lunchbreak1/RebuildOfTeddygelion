using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    public TextMeshProUGUI giveNameLabel;

    TrickManager trickManager;

    public float messageDuration;

    public GameObject savePanel, playAgainPanel;

    // Start is called before the first frame update
    void Start()
    {
        trickManager = FindAnyObjectByType<TrickManager>();

        if (trickManager == null) {
            Debug.LogError("Could not find TrickManager in SaveMenu");
        }
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(playerName.text))
        {
            giveNameLabel.text = "<color=red><shake>Please give your file a name.</color></shake>";
        }
        else
        {
            Debug.Log("Here's your name: " + playerName.text);
            SaveData data = new SaveData(playerName.text, trickManager.score);
            trickManager.WriteToTrickCounter("<color=green><shake>File saved!</color></shake>", messageDuration);
            savePanel.SetActive(false);
            playAgainPanel.SetActive(true);
        }  
    }

    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
