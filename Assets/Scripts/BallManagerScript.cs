using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagerScript : MonoBehaviour
{
    Dictionary<BallScript, Vector2> balls = new Dictionary<BallScript, Vector2>();
    
    List<BallScript> toRemove = new List<BallScript>();

    private static int pauseGame = 0;

    public static BallManagerScript instance;
    public bool checkFinished = true;

    public int Count { get => balls.Count + PauseGame; }

    public static int PauseGame
    {
        get => pauseGame;
        set
        {
            pauseGame = value;
            if (pauseGame == 0)
                instance.CheckZeroMovement();
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        MoveBalls();
    }

    public void CheckZeroMovement()
    {
        if (PauseGame != 0)
            return;

        checkFinished = false;

        if (balls.Count == 0)
            FieldScript.instance.CheckTrippel();

        if (balls.Count == 0)
            FieldScript.instance.SetBalances();

        if (balls.Count == 0)
            foreach (CollumeScript collum in FieldScript.instance.collums)
            {
                collum.CheckStack();
            }

        checkFinished = true;
    }

    public void AddDestroyedBall(BallScript ball)
    {
        if (balls.ContainsKey(ball) && !toRemove.Contains(ball))
        {
            toRemove.Add(ball);
        }
    }

    public void AddBallsMovement(BallScript ball, Vector2 position)
    {
        if (balls.ContainsKey(ball))
        {
            balls[ball] = position;
            return;
        }
        balls.Add(ball, position);
    }

    private void MoveBalls()
    {
        if (balls.Count == 0)
            return;

        RemoveBalls();

        //Moves Balls
        foreach (KeyValuePair<BallScript, Vector2> item in balls)
        {
            BallScript ball = item.Key;
            Vector3 pos = item.Value;

            pos = new Vector3(pos.x * 2, pos.y);
            if (ball == null)
                continue;

            float distance = Vector2.Distance(ball.transform.position, pos);

            //MoveBall Direction
            Vector3 direction = pos - ball.transform.position;

            //MoveBall
            ball.transform.position += direction * Time.deltaTime * 15;

            if (distance <= .3f)
            {
                ball.transform.position = pos;
                toRemove.Add(ball);
            }
        }

        if (balls.Count == 0)
            CheckZeroMovement();
    }

    private void RemoveBalls()
    {
        if (PauseGame != 0)
            return;

        //Removes Balls
        for (int i = 0; i < toRemove.Count; i++)
        {
            BallScript ball = toRemove[i];
            

            if (!balls.ContainsKey(ball))
                continue;

            if (ball.ability != null && PauseGame == 0)
            {
                Vector3 pos = balls[ball];
                if (!ball.activated && ball.transform.position.y < 8.4)
                {
                    StartCoroutine(ball.ability.CAbility((int)pos.x, (int)pos.y, (int)pos.z));
                }
            }

            //delete ball
            if (ball.done)
            {
                StorageManagerScript.instance.SetBall(ball);
            }

            balls.Remove(ball);
        }

        toRemove.Clear();
        toRemove.TrimExcess();
    }
}
