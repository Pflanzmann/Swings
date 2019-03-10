using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerScript : MonoBehaviour
{
    List<BallScript> balls = new List<BallScript>();
    List<BallScript> tempBalls = new List<BallScript>();

    List<BallScript> toRemove = new List<BallScript>();
    List<BallScript> toTempRemove = new List<BallScript>();

    public int Count { get => balls.Count + tempBalls.Count; }


    public static ChangerScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        MoveBallsUp();
        JumpBall();
    }

    public void AddBallsMovement(BallScript ball)
    {
        tempBalls.Add(ball);
    }

    private void MoveBallsUp()
    {
        if (tempBalls.Count == 0)
            return;

        foreach (BallScript ball in tempBalls)
        {
            if (ball.transform.position.y < 8.5)
            {
                ball.transform.position += Vector3.up * Time.deltaTime * 20;
            }

            if (Vector2.Distance(ball.transform.position, new Vector2(ball.transform.position.x, 8.5f)) <= 0.35)
            {
                ball.transform.position = new Vector2(ball.transform.position.x, 8.5f);

                toTempRemove.Add(ball);
            }
        }

        foreach (BallScript ball in toTempRemove)
        {
            balls.Add(ball);
            tempBalls.Remove(ball);
        }
        toTempRemove.Clear();
        toTempRemove.TrimExcess();
    }

    private void JumpBall()
    {
        if (balls.Count == 0)
            return;

        foreach (BallScript ball in balls)
        {
            int startCollume = ball.StartCollume;
            int momentum = ball.Momentum;
            Vector3 direction = Vector3.right;

            if (startCollume > 3)
            {
                direction = Vector3.left;
            }

            int target = (startCollume * 2) + (momentum * 2);

            if (target > 14)
                target += 10;

            if (target < -0)
                target -= 10;

            ball.transform.position += direction * Time.deltaTime * 15;

            if (ball.transform.position.x >= 17 && direction == Vector3.right)
            {
                ball.transform.position = new Vector2(-3, 8.5f);

                momentum -= 8 - startCollume;
                ball.Momentum = momentum;

                startCollume = 0;
                ball.StartCollume = startCollume;

                target = (startCollume * 2) + (momentum * 2);

                if (target > 14)
                    target += 4;
            }
            else if (ball.transform.position.x <= -3 && direction == Vector3.left)
            {
                ball.transform.position = new Vector2(17, 8.5f);

                momentum += startCollume + 1;
                ball.Momentum = momentum;

                startCollume = 7;
                ball.StartCollume = startCollume;

                target = (startCollume * 2) + (momentum * 2);

                if (target < -3)
                    target -= 4;
            }

            if (Vector2.Distance(ball.transform.position, new Vector2(target, 8.5f)) <= 0.5)
            {
                ball.transform.position = new Vector2(Mathf.RoundToInt(ball.transform.position.x / 2) * 2, 8.5f);

                toRemove.Add(ball);
            }

        }

        foreach (BallScript ball in toRemove)
        {
            int collumeToSet = Mathf.Clamp(Mathf.RoundToInt(ball.transform.position.x / 2), 0, 7);
            FieldScript.instance.SetBall(ball, collumeToSet);
            balls.Remove(ball);
        }
        toRemove.Clear();
        toRemove.TrimExcess();
    }
}
