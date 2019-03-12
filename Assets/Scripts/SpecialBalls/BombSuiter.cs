using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/BombSuiter")]
public class BombSuiter : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        int color = FieldScript.instance.collums[collume].ReturnColor(row - 1, offset);
        BallManagerScript.PauseGame++;

        if (color <= 0)
        {
            FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

            BallManagerScript.PauseGame--;
            yield break;
        }

        if (collume > 0)
        {
            for (int i = 1; i > -2; i--)
            {
                if (FieldScript.instance.collums[collume - 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume - 1].ReturnBall(row + i, offset).CurrentColor = color;
                }
            }
        }

        for (int i = 1; i > -2; i--)
        {
            if (FieldScript.instance.collums[collume].ReturnBall(row + i, offset) != null)
            {
                FieldScript.instance.collums[collume].ReturnBall(row + i, offset).CurrentColor = color;
            }
        }

        if (collume < FieldScript.instance.collums.Count - 1)
        {
            for (int i = 1; i > -2; i--)
            {
                if (FieldScript.instance.collums[collume + 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume + 1].ReturnBall(row + i, offset).CurrentColor = color;
                }
            }
        }

        yield return new WaitForSeconds(0.7f);
        BallManagerScript.PauseGame--;
        yield break;
    }
}
