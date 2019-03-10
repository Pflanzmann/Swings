using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/ColorJoker")]
public class ColorJoker : BasisSpecialBallScript
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

        List<Vector2Int> balls = new List<Vector2Int>();

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < FieldScript.instance.collums[j].Lenght; i++)
            {
                if (FieldScript.instance.collums[j].ReturnColor(i) == color)
                {
                    FieldScript.instance.collums[j].ReturnBall(i).CurrentColor = -1;
                }
            }
        }

        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        yield return new WaitForSeconds(.7f);

        BallManagerScript.PauseGame--;
        yield break;
    }
}
