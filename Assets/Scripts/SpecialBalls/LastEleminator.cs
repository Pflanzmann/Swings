using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/LastEleminator")]
public class LastEleminator : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);
        BallManagerScript.PauseGame++;

        for (int j = 0; j < 8; j++)
        {
            int newOffset = FieldScript.instance.collums[j].Offset;

            int i = FieldScript.instance.collums[j].Lenght - 1;

            if (i >= 0)
                FieldScript.instance.collums[j].DeleteBallInstant(i, newOffset);
            yield return new WaitForSeconds(.05f);
        }

        BallManagerScript.PauseGame--;
        //BallManagerScript.instance.CheckZeroMovement();
        yield break;
    }
}
