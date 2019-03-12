using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    int fallenBalls = 0;
    int levelProgress = 0;
    int currentDiversity = 4;
    int points = 0;
    int additionalCurrency = 0;

    HighscoreTable table;
    public static StatusController instance;
    private bool canSave = true;
    private bool pointsChanged = true;

    PlayerData currentPlayer;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadHighscores();
        AddPoints(0, 0);
    }

    void LateUpdate()
    {
        if (!pointsChanged)
            return;

        ShopScript.instance.Currency += additionalCurrency;
        additionalCurrency = 0;
        UIManagerScript.instance.ShowPoints(points);
        pointsChanged = false;
    }

    public void AddFallenBalls()
    {
        fallenBalls++;
        levelProgress++;

        if (levelProgress % (currentDiversity * 10) == 0)
        {
            currentDiversity++;
            SpawnerScript.instance.CurrentLevel = currentDiversity;
            UIManagerScript.instance.ShowLevel(currentDiversity);
            levelProgress = 0;

            ShopScript.instance.ShuffelShop();
        }
    }

    public void AddPoints(int value, int color)
    {
        points += currentDiversity * (2 * value);
        additionalCurrency += value;
        pointsChanged = true;
    }

    public void PlayerDieded(BallScript ball)
    {
        BallManagerScript.instance.enabled = false;
        ChangerScript.instance.enabled = false;
        GrabberScript.instance.enabled = false;
        FieldScript.instance.enabled = false;
        BallManagerScript.instance.enabled = false;

        SaveHighscore();

        StartCoroutine(UIManagerScript.instance.MoveEndsccreenIn(ball));
    }

    public void SaveHighscore()
    {
        if (!canSave)
            return;

        canSave = false;

        table.lastPlayer = new PlayerData(points, table.standartName);

        for (int i = 0; i < table.datas.Count; i++)
        {
            if (table.lastPlayer.highscore == 0)
            {
                table.datas.Add(table.lastPlayer);
                table.lastPlayerIndex = table.datas.Count - 1;
                break;
            }

            if (table.datas[i].highscore <= table.lastPlayer.highscore)
            {
                table.datas.Insert(i, table.lastPlayer);
                table.lastPlayerIndex = i;
                break;
            }
        }

        SaveSystemScript.SaveTable(table);
        LoadHighscores();
    }

    public void ChangeLastPlayerName(string input)
    {
        if (input.Contains("Reset"))
        {
            SaveSystemScript.SaveTable(new HighscoreTable());
            LoadHighscores();
            UIManagerScript.instance.ShowHighscoreDeathscreen(table);
            return;
        }
        if (input.Length <= 0)
            return;

        table.datas[table.lastPlayerIndex].name = input;
        table.standartName = input;

        SaveSystemScript.SaveTable(table);
        UIManagerScript.instance.ShowHighscoreDeathscreen(table);
    }

    public void LoadHighscores()
    {
        table = SaveSystemScript.LoadScore();

        if (table == null)
        {
            table = new HighscoreTable();
            SaveSystemScript.SaveTable(table);
        }
        UIManagerScript.instance.ShowHighscoreDeathscreen(table);
    }
}
