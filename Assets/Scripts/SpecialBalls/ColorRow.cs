using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/ColorRow")]
public class ColorRow : BasisSpecialBallScript
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

        for (int j = 0; j < 8; j++)
        {
            if (FieldScript.instance.collums[j].ReturnBall(row, offset) != null && color > 0)
            {
                FieldScript.instance.collums[j].ReturnBall(row, offset).CurrentColor = color;
                yield return new WaitForSeconds(.05f);
            }
        }

        yield return new WaitForSeconds(.3f);
        BallManagerScript.PauseGame--;
        yield break;
    }
}
