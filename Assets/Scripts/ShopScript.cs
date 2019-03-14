using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static ShopScript instance;

    public int ball1 = 1;
    int costBall1 = 100;

    public int ball2;
    int costBall2 = 12;

    public int ball3;
    int costBall3 = 12;

    int costShuffel = 10;

    int currency = 0;
    int last1 = 0;
    int last2 = 0;


    public int Currency
    {
        get { return currency; }
        set
        {
            currency = value;
            UIManagerScript.instance.shopCurrencyText.text = currency + "$";
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        ShuffelShop();
    }

    public void ButtonMethod(int button)
    {
        switch (button)
        {
            case 1:
                if (Currency >= costBall1)
                {
                    Currency -= costBall1;
                    costBall1 += 50;
                    SpawnSpecialBall();

                    GrabberScript.instance.currentBall.CurrentColor = -ball1;
                }
                break;

            case 2:
                if (Currency >= costBall2)
                {
                    Currency -= costBall2;
                    SpawnSpecialBall();

                    GrabberScript.instance.currentBall.CurrentColor = -ball2;

                    ShuffelShop();
                }
                break;

            case 3:
                if (Currency >= costBall3)
                {
                    Currency -= costBall3;
                    SpawnSpecialBall();

                    GrabberScript.instance.currentBall.CurrentColor = -ball3;

                    ShuffelShop();
                }
                break;

            case 4:
                if (Currency >= costShuffel)
                {
                    Currency -= costShuffel;
                    costShuffel *= 2;
                    ShuffelShop();

                    UIManagerScript.instance.shopShuffelPriceText.text = costShuffel + "$";
                }
                break;
        }
    }

    public void SpawnSpecialBall()
    {
        //Get rid of old ball
        BallScript ball = GrabberScript.instance.currentBall;
        ball.done = true;
        BallManagerScript.instance.AddBallsMovement(ball, new Vector3(-1, 8.5f, 0));

        //get new ball
        ball = StorageManagerScript.instance.GetBall();
        GrabberScript.instance.currentBall = ball;

        ball.transform.position = new Vector3(16, 8.5f, 0);
        ball.transform.SetParent(GrabberScript.instance.transform);

        //move new ball to grabber
        Vector3 grabberPos = new Vector3(GrabberScript.instance.transform.position.x / 2, 8.5f, 0);
        BallManagerScript.instance.AddBallsMovement(ball, grabberPos);

        ShowCosts();
    }

    public void ShuffelShop()
    {
        while (ball2 == last1 || ball2 == last2 || ball3 == last1 || ball3 == last2 || ball2 == ball3)
        {
            ball2 = Random.Range(2, SpriteHolder.instance.abilitys.Length);
            ball3 = Random.Range(2, SpriteHolder.instance.abilitys.Length);
        }

        last1 = ball2;
        last2 = ball3;
        costBall2 = ball2 * 100;
        costBall3 = ball3 * 100;

        ShowCosts();
    }

    private void ShowCosts()
    {
        UIManagerScript.instance.shopShuffelPriceText.text = costShuffel + "$";
        UIManagerScript.instance.shopJokerPrice.text = costBall1 + "$";
        UIManagerScript.instance.shopBall1PriceTet.text = costBall2 + "$";
        UIManagerScript.instance.shopBall2PriceTet.text = costBall3 + "$";

        UIManagerScript.instance.ball1Image.sprite = SpriteHolder.instance.specialballsSprites[ball1];
        UIManagerScript.instance.ball2Image.sprite = SpriteHolder.instance.specialballsSprites[ball2];
        UIManagerScript.instance.ball3Image.sprite = SpriteHolder.instance.specialballsSprites[ball3];
    }
}
