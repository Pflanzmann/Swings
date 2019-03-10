using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/StackMaker")]
public class StackMaker : BasisSpecialBallScript
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

        int length = FieldScript.instance.collums[collume].Lenght;

        if (color > 0)
            for (int i = length - 2; i > -1; i--)
            {
                FieldScript.instance.collums[collume].ReturnBall(i).CurrentColor = color;
                yield return null;
            }

        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        yield return new WaitForSeconds(0.3f);

        BallManagerScript.PauseGame--;
        yield break;
    }
}
