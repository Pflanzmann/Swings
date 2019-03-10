using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollumeScript : MonoBehaviour
{
    List<BallScript> balls = new List<BallScript>();

    TextMeshPro weightText;
    public Animation shakeAnimation;

    int index = 0;
    int offset = 0;
    int value = 0;

    public int Index { get => index; set => index = value; }
    public int Value { get => value; private set => this.value = value; }
    public int Lenght { get => balls.Count; }
    public int Offset { get => offset; set => offset = value; }

    public void AddBall(BallScript ball)
    {
        balls.Add(ball);
        int ballIndex = balls.IndexOf(ball);

        BallManagerScript.instance.AddBallsMovement(ball,
            new Vector2(
                Index,
                ballIndex + Offset));

        ball.transform.SetParent(transform);
    }

    public void SetWeightDisplay(GameObject display)
    {
        weightText = display.GetComponentInChildren<TextMeshPro>();
        shakeAnimation = weightText.gameObject.GetComponent<Animation>();
    }

    public void DeleteBallForStack(int row)
    {

        balls[row].done = true;
        balls.Remove(balls[row]);

        //balls[row] = null;
        //balls.Remove(null);

        for (int i = row; i < balls.Count; i++)
        {
            BallManagerScript.instance.AddBallsMovement(balls[i],
               new Vector2(
                   Index,
                   i + offset));
        }
    }

    public void DeleteBallInstant(int row, int newOffset)
    {
        newOffset = newOffset - Offset;

        //Add points to the Highscore
        StatusController.instance.AddPoints(balls[row + newOffset].Value, balls[row + newOffset].CurrentColor);

        //Deletes ball from the Ballmanager
        BallManagerScript.instance.AddDestroyedBall(balls[row + newOffset]);

        //Deletes Ball
        PlayDeathParticle(balls[row + newOffset]);

        StorageManagerScript.instance.SetBall(balls[row + newOffset]);

        balls.Remove(balls[row + newOffset]);

        //balls[row + newOffset] = null;
        //balls.Remove(null);

        for (int i = row + newOffset; i < balls.Count; i++)
        {
            BallManagerScript.instance.AddBallsMovement(balls[i],
               new Vector2(
                   Index,
                   i + Offset));
        }
    }

    public void MoveLastBallUp(int momentum)
    {
        if (balls.Count <= 0)
            return;

        int row = balls.Count - 1;

        balls[row].transform.SetParent(null);

        if (Index > 3)
            balls[row].Momentum = -momentum;
        else
            balls[row].Momentum = momentum;

        balls[row].StartCollume = Index;
        ChangerScript.instance.AddBallsMovement(balls[row]);
        balls.Remove(balls[row]);
        //balls[row] = null;
        //balls.Remove(null);
    }

    public int CalculateValue()
    {
        Value = 0;
        for (int i = 0; i < balls.Count; i++)
        {
            Value += balls[i].Value;
        }
        weightText.text = Value.ToString();
        return Value;
    }

    public void SetBalancePosition(int newOffset, int momentum)
    {
        if (Offset == newOffset)
            return;
        if (offset - newOffset == -2)
            MoveLastBallUp(momentum);

        Offset = newOffset;

        BallManagerScript.instance.AddBallsMovement(gameObject.GetComponent<BallScript>(),
           new Vector2(
               Index,
               -1.55f + Offset));
    }

    public void CheckStack()
    {
        int count = 0;
        int color = 0;
        int startRow = 0;

        for (int i = 0; i < balls.Count; i++)
        {
            if (i == 0)
            {
                color = balls[i].CurrentColor;
                startRow = i;
                count = 1;
            }
            else if (ReturnColor(i) == color)
            {
                count++;
            }
            else if (ReturnColor(i) != color && ReturnColor(i) > 0 && count < 5)
            {
                color = ReturnColor(i);
                startRow = i;
                count = 1;
            }
        }

        if (count >= 5)
        {
            for (int j = count - 1; j > 0; j--)
            {
                balls[startRow].Value += balls[startRow + j].Value;
                BallManagerScript.instance.AddBallsMovement(balls[startRow + j],
                    new Vector2(
                    Index,
                    startRow + offset));

                DeleteBallForStack(startRow + j);

            }
            PlayDeathParticle(balls[startRow]);
        }

        if (balls.Count + Offset >= 9)
        {
            StatusController.instance.PlayerDieded(balls[Lenght - 1]);
        }
    }

    public BallScript ReturnBall(int row, int newOffset)
    {
        newOffset = newOffset - offset;

        if (row + newOffset >= balls.Count || row + newOffset < 0)
            return null;

        return balls[row + newOffset];
    }

    public BallScript ReturnBall(int row)
    {
        if (row >= balls.Count)
            return null;

        return balls[row];
    }

    public int ReturnColor(int row, int newOffset)
    {
        newOffset = newOffset - offset;

        if (row + newOffset >= balls.Count || row + newOffset < 0)
            return 0;

        return balls[row + newOffset].CurrentColor;
    }

    public int ReturnColor(int row)
    {
        if (row >= balls.Count || row < 0)
            return 0;

        return balls[row].CurrentColor;
    }

    public void PlayDeathParticle(BallScript ball)
    {
        ball.SetDeathParticleAndPlay();
    }
}
