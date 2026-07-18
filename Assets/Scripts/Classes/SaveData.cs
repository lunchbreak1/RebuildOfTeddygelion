[System.Serializable]
public class SaveData
{
    public string playerName;
    public string level;
    public float score;
    public int threeSixties;
    public int flips;
    public int backFlips;
    public int wipeouts;
    public int railGrinds;
    public int fallOffRails;
    public int posters;
    public int collectables;


    public SaveData(string name, float newScore, string levelName, int threeSixties, int flips, int backFlips, int wipeouts, int railGrinds, int fallOffRails, int posters, int collectables)
    {
        playerName = name;
        score = newScore;
        level = levelName;
        this.threeSixties = threeSixties;
        this.flips = flips;
        this.backFlips = backFlips;
        this.wipeouts = wipeouts;
        this.railGrinds = railGrinds;
        this.fallOffRails = fallOffRails;
        this.posters = posters;
        this.collectables = collectables;
    }
}