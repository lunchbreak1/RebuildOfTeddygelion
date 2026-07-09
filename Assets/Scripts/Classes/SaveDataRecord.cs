using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveDataRecord : MonoBehaviour
{
    /// <summary>
    /// Text field showing the name of the player
    /// </summary>
    public TextMeshProUGUI nameText;

    /// <summary>
    /// Text field showing the score the player has
    /// </summary>
    public TextMeshProUGUI levelText;

    /// <summary>
    /// Text field showing the record's level
    /// </summary>
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(SaveData data)
    {
        SetName(data.playerName);

        SetScore(data.score);

        SetLevel(data.level);
    }

    /// <summary>
    /// Set the name
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        nameText.text = name;
    }

    /// <summary>
    /// Set the level
    /// </summary>
    /// <param name="name"></param>
    public void SetLevel(string level)
    {
        levelText.text = level;
    }

    /// <summary>
    /// Set the name
    /// </summary>
    /// <param name="name"></param>
    public void SetScore(float score)
    {
        scoreText.text = "" + score;
    }


}
