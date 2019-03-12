using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/BlackHole")]
public class BlackHole : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        BallScript ball = FieldScript.instance.collums[collume].ReturnBall(row, offset);
        BallManagerScript.PauseGame++;
        ball.activated = true;

        int counter = 0;

        BallManagerScript.PauseGame--;

        while (counter < 5)
        {
            if(FieldScript.instance.collums[collume].ReturnBall(row + 1, offset) != null)
            {
                FieldScript.instance.collums[collume].DeleteBallInstant(row + 1, offset);
                counter++;
            }

            yield return new WaitForSeconds(5);
        }


        FieldScript.instance.collums[collume].ReturnBall(row + 1, offset).done = true;
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        yield break;
    }
}
