using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;

    [SerializeField] TextMeshProUGUI ingamePointsText;
    [SerializeField] TextMeshProUGUI ingameLevelText;
    [Space]
    [Header("Death Screen")]
    [SerializeField] Image deathScreenImage;
    [SerializeField] TextMeshProUGUI topPointsText;
    [SerializeField] TMP_InputField topNameText;
    [SerializeField] TextMeshProUGUI highscoreNameText;
    [SerializeField] TextMeshProUGUI highscoreNaumberText;
    [SerializeField] TextMeshProUGUI highscorePointsText;
    [SerializeField] TextMeshProUGUI highscoreDateText;
    [Space]
    [Header("Shop Window")]
    public TextMeshProUGUI shopCurrencyText;
    public TextMeshProUGUI shopJokerPrice;
    public TextMeshProUGUI shopBall1PriceTet;
    public TextMeshProUGUI shopBall2PriceTet;
    public TextMeshProUGUI shopShuffelPriceText;
    [SerializeField] Image shopWindow;
    public Image ball1Image;
    public Image ball2Image;
    public Image ball3Image;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowPoints(0);
    }

    public void ShowLevel(int level)
    {
        ingameLevelText.text = "Level: " + level;
    }

    public void ShowPoints(int points)
    {
        ingamePointsText.text = "Highscore\n" + points.ToString();

        topPointsText.text = points.ToString();
    }

    public IEnumerator MoveEndsccreenIn(BallScript ball)
    {
        yield return new WaitForSeconds(1);

        float scale = 0.068f;
        int color = ball.CurrentColor;

        if (color > 0)
        {
            Color color1 = SpriteHolder.instance.firstColors[color];
            Color color2 = SpriteHolder.instance.secondColors[color];

            deathScreenImage.material.SetColor("_Color1", color1);
            deathScreenImage.material.SetColor("_Color2", color2);
        }
        else
        {
            Color color1 = new Color(1, 1, 1, 1);
            Color color2 = new Color(1, 1, 1, 1);

            deathScreenImage.material.SetColor("_Color1", new Color(0, 0, 0, 0));
            deathScreenImage.material.SetColor("_Color2", new Color(0, 0, 0, 0));

            deathScreenImage.sprite = SpriteHolder.instance.specialballsSprites[-color];
        }


        Vector2 position = ball.transform.position;
        Vector2 position2 = new Vector2(7, 5);

        deathScreenImage.rectTransform.localScale = new Vector2(scale, scale);
        deathScreenImage.rectTransform.position = position;


        while (scale < 1)
        {
            scale += Time.deltaTime * 2;
            deathScreenImage.rectTransform.localScale = new Vector2(scale, scale);

            deathScreenImage.rectTransform.position = (position * (1 - scale)) + (position2 * scale);

            yield return null;
        }
        deathScreenImage.rectTransform.position = position2;

        topNameText.gameObject.SetActive(true);
        yield break;
    }

    public void EnterInputField()
    {
        StatusController.instance.ChangeLastPlayerName(topNameText.text);
    }

    public void LoadLevelWithGameMaster(int level)
    {
        MenuScript.instance.OpenSceneWithButton(level);
    }

    public void ShowHighscoreDeathscreen(HighscoreTable table)
    {
        topNameText.placeholder.GetComponent<TextMeshProUGUI>().text = table.standartName;

        string a = "Name\n";
        string b = "Points\n";
        string c = "Date\n";
        string d = "\n";
        int i = 1;
        if (table != null)
            foreach (PlayerData item in table.datas)
            {
                a += item.name + "\n";
                b += item.highscore + "\n";
                c += item.date.ToShortDateString() + "\n";
                d += i++ + ")\n";

                if (i == 11)
                {
                    if (table.lastPlayer?.highscore < item.highscore)
                    {
                        a += "...\n";
                        b += "...\n";
                        c += "...\n";
                        d += "...\n";

                        a += table.lastPlayer.name + "\n";
                        b += table.lastPlayer.highscore + "\n";
                        c += table.lastPlayer.date.ToShortDateString() + "\n";
                        d += (table.datas.IndexOf(table.lastPlayer) + 1) + ")\n";
                    }

                    break;
                }
            }
        highscoreNameText.text = a;
        highscorePointsText.text = b;
        highscoreDateText.text = c;
        highscoreNaumberText.text = d;
    }
}
