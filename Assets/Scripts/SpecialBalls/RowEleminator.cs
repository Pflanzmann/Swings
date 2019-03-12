using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/RowEleminator")]
public class RowEleminator : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);
        BallManagerScript.PauseGame++;

        for (int j = 0; j < 8; j++)
        {
            if (FieldScript.instance.collums[j].ReturnBall(row, offset) != null)
            {
                FieldScript.instance.collums[j].DeleteBallInstant(row, offset);
                yield return new WaitForSeconds(.05f);
            }
        }

        BallManagerScript.PauseGame--;
        yield break;
    }
}
