[System.Serializable]
public class SaveData
{
    public string playerName;
    public string level;
    public float score;

    public SaveData(string name, float newScore, string levelName)
    {
        playerName = name;
        score = newScore;
        level = levelName;
    }
}