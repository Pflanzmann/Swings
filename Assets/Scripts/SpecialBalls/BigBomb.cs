using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/BigBomb")]
public class BigBomb : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);
        BallManagerScript.PauseGame++;

        //-2 left
        if (collume > 1)
        {
            for (int i = 2; i > -3; i--)
            {
                if (FieldScript.instance.collums[collume - 2].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume - 2].DeleteBallInstant(row + i, offset);
                }
            }
        }

        //-1 left
        if (collume > 0)
        {
            for (int i = 2; i > -3; i--)
            {
                if (FieldScript.instance.collums[collume - 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume - 1].DeleteBallInstant(row + i, offset);
                }
            }
        }

        //middle
        for (int i = 2; i > -3; i--)
        {
            if (FieldScript.instance.collums[collume].ReturnBall(row + i, offset) != null)
            {
                FieldScript.instance.collums[collume].DeleteBallInstant(row + i, offset);
            }
        }

        //+1 right
        if (collume < FieldScript.instance.collums.Count - 1)
        {
            for (int i = 2; i > -3; i--)
            {
                if (FieldScript.instance.collums[collume + 1].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume + 1].DeleteBallInstant(row + i, offset);
                }
            }
        }   
        
        //+2 right
        if (collume < FieldScript.instance.collums.Count - 2)
        {
            for (int i = 2; i > -3; i--)
            {
                if (FieldScript.instance.collums[collume + 2].ReturnBall(row + i, offset) != null)
                {
                    FieldScript.instance.collums[collume + 2].DeleteBallInstant(row + i, offset);
                }
            }
        }

        BallManagerScript.PauseGame--;
        yield break;
    }
}
