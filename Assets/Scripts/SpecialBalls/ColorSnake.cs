using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/ColorSnake")]
public class ColorSnake : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        int color = FieldScript.instance.collums[collume].ReturnColor(row - 1, offset);

        if (color <= 0)
        {
            FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

            BallManagerScript.PauseGame--;
            yield break;
        }

        BallScript snake = FieldScript.instance.collums[collume].ReturnBall(row, offset);
        snake.activated = true;

        int tempCollume = collume;
        int tempRow = row;
        int tempOffset = offset;

        int lastChoose = 0;
        int choose = 0;

        BallManagerScript.instance.AddBallsMovement(snake, new Vector2(collume, row));

        yield return new WaitForSeconds(.4f);

        while (tempRow - FieldScript.instance.collums[tempCollume].Offset > 0)
        {

        test:
            switch (lastChoose)
            {
                case -1:
                    if (tempCollume > 0)
                        choose = Random.Range(0, 2) - 1;
                    else
                        choose = 0;
                    break;

                case 0:
                    if (tempCollume < 7 && tempCollume > 0)
                        choose = Random.Range(0, 3) - 1;
                    else if(tempCollume == 0)
                        choose = Random.Range(0, 2);
                    else if(tempCollume == 7)
                        choose = Random.Range(0, 2) -1;
                    break;

                case 1:
                    if (tempCollume < 7)
                        choose = Random.Range(0, 2);
                    else
                        choose = 0;
                    break;
            }

            switch (choose)
            {
                case -1:
                    tempCollume--;
                    break;
                case 0:
                    tempRow--;
                    break;
                case 1:
                    tempCollume++;
                    break;
            }


            if (FieldScript.instance.collums[tempCollume].ReturnBall(tempRow, tempOffset) != null)
            {
                BallManagerScript.instance.AddBallsMovement(snake, new Vector2(tempCollume, tempRow));

                FieldScript.instance.collums[tempCollume].ReturnBall(tempRow, tempOffset).CurrentColor = color;
            }
            else
            {
                switch (choose)
                {
                    case -1:
                        tempCollume++;
                        break;
                    case 0:
                        tempRow++;
                        break;
                    case 1:
                        tempCollume--;
                        break;
                }
                goto test;
            }
            lastChoose = choose;
            yield return new WaitForSeconds(0.2f);

            if (tempRow - tempOffset == 0)
                break;
        }

        yield return new WaitForSeconds(0.7f);

        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);
        BallManagerScript.instance.AddDestroyedBall(snake);

        BallManagerScript.PauseGame--;
        yield break;
    }
}
