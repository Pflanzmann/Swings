using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/Bomb")]
public class Bomb : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        if (collume > 0)
        {
            for (int i = 1; i > -2; i--)
            {
                if (FieldScript.instance.collums[collume - 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume - 1].DeleteBallInstant(row + i, offset);
                }
            }
        }

        for (int i = 1; i > -2; i--)
        {
            if (FieldScript.instance.collums[collume].ReturnBall(row + i, offset) != null)
            {
                FieldScript.instance.collums[collume].DeleteBallInstant(row + i, offset);
            }
        }

        if (collume < FieldScript.instance.collums.Count - 1)
        {
            for (int i = 1; i > -2; i--)
            {
                if (FieldScript.instance.collums[collume + 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume + 1].DeleteBallInstant(row + i, offset);
                }
            }
        }

        BallManagerScript.PauseGame--;
        yield break;
    }
}
