using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/Tornado")]
public class Tornado : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);
        BallManagerScript.PauseGame++;

        int length = FieldScript.instance.collums[collume].Lenght;
        ;
        int momentum;

        while (length != 0)
        {
            int random1 = Random.Range(0, 5);
            int random2 = Random.Range(1, 5);

            momentum = FieldScript.instance.collums[collume].ReturnBall(length - 1).Value + (random1 * random2);

            FieldScript.instance.collums[collume].MoveLastBallUp(momentum);
            length = FieldScript.instance.collums[collume].Lenght;
            yield return new WaitForSeconds(0.05f);
        }


        BallManagerScript.PauseGame--;
        yield break;
    }
}
