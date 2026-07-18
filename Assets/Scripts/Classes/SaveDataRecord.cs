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

    public TextMeshProUGUI threeSixtiesText;
    public TextMeshProUGUI flipsText;
    public TextMeshProUGUI backflipsText;
    public TextMeshProUGUI wipeoutsText;
    public TextMeshProUGUI railGrindsText;
    public TextMeshProUGUI fallOffRailsText;
    public TextMeshProUGUI postersText;
    public TextMeshProUGUI collectablesText;

    public void Create(SaveData data)
    {
        SetText(data.playerName, nameText);

        SetText(data.score, scoreText);

        SetText(data.level, levelText);
        SetText(data.threeSixties, threeSixtiesText);
        SetText(data.flips, flipsText);
        SetText(data.backFlips, backflipsText);
        SetText(data.wipeouts, wipeoutsText);
        SetText(data.railGrinds, railGrindsText);
        SetText(data.fallOffRails, fallOffRailsText);
        SetText(data.posters, postersText);
        SetText(data.collectables, collectablesText);
    }

    public void SetText(string text, TextMeshProUGUI textMesh)
    {
        textMesh.text = text;
    }

    public void SetText(float text, TextMeshProUGUI textMesh)
    {
        textMesh.text = text + "";
    }

    public void SetText(int text, TextMeshProUGUI textMesh)
    {
        textMesh.text = text + "";
    }
}
