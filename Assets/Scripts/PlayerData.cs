

[System.Serializable]
public class PlayerData
    {
    public int fallenBalls;
    public int level;
    public int highscore;
    public string name;
    public System.DateTime date;

    public PlayerData(int highscore, string name)
    {
        this.highscore = highscore;
        this.name = name;
        this.date = System.DateTime.Now;
    }

    public override string ToString()
    {
        return highscore + " \t\t\t\t" + name + " \t\t"  + date + "\n";
    }
}

