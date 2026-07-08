using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public TextMeshProUGUI playerName;

    public TextMeshProUGUI label;

    TrickManager trickManager;

    public float messageDuration;

    // Start is called before the first frame update
    void Start()
    {
        trickManager = FindAnyObjectByType<TrickManager>();

        if (trickManager != null) {
            Debug.LogError("Could not find TrickManager in SaveMenu");
        }
    }

    public void Save()
    {
        if (playerName.text == "")
        {
            label.text = "<color=red><shake>Please give your file a name.</color></shake>";
        }
        else
        {
            SaveData data = new SaveData(playerName.text, trickManager.score);
            trickManager.WriteToTrickCounter("<color=green><shake>File saved!</color></shake>", messageDuration);
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
