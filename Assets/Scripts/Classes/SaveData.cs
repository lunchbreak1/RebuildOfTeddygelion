[System.Serializable]
public class SaveData
{
    public string playerName;
    public float score;

    public SaveData(string name, float newScore)
    {
        playerName = name;
        score = newScore;
        SaveManager.Save(this);
    }
}