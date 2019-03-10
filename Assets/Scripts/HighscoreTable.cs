

using System.Collections.Generic;

[System.Serializable]
public class HighscoreTable
{
    public PlayerData lastPlayer = null;
    public string standartName = "Name...";
    public int lastPlayerIndex = 0;

    public List<PlayerData> datas = new List<PlayerData>();

    public HighscoreTable()
    {
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
        datas.Add(new PlayerData(1000, "Foo"));
    }
}

