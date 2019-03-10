using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    BallScript[,] balls = new BallScript[9, 2];
    int currentLevel = 4;

    public static SpawnerScript instance;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        TestSpawns();
    }

    private void GetBall(Vector2Int pos)
    {
        balls[pos.x, pos.y] = StorageManagerScript.instance.GetBall();
        balls[pos.x, pos.y].transform.position = new Vector2(pos.x * 2f, pos.y + 13);

        PopulateBall(balls[pos.x, pos.y]);

        BallManagerScript.instance.AddBallsMovement(balls[pos.x, pos.y], new Vector2(pos.x, pos.y + 10));
    }

    public void ChangeBall(int collume, int row, int color)
    {
        balls[collume, row].CurrentColor = color;

        balls[collume, row].SetDeathParticleAndPlay();
    }

    public BallScript MoveDown(int collume)
    {
        //Ball to return
        BallScript temp = balls[collume, 0];

        balls[collume, 0] = balls[collume, 1];

        BallManagerScript.instance.AddBallsMovement(balls[collume, 0], new Vector2(collume, 10));

        GetBall(new Vector2Int(collume, 1));
        return temp;
    }

    private void TestSpawns()
    {
        for (int i = 0; i < 8; i++)
        {
            GetBall(new Vector2Int(i, 0));
            GetBall(new Vector2Int(i, 1));
        }
    }

    public void PopulateBall(BallScript ball)
    {
        ball.CurrentColor = Random.Range(1, CurrentLevel + 1);

        ball.Value = Random.Range(1, CurrentLevel + 1);
    }
}
