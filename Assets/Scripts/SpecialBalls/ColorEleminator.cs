using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/ColorEleminator")]
public class ColorEleminator : BasisSpecialBallScript
{
    public override IEnumerator CAbility(int collume, int row, int offset)
    {
        int color = FieldScript.instance.collums[collume].ReturnColor(row - 1, offset);
        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        if (color <= 0)
        {
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
                    balls.Add(new Vector2Int(j, i));
                }
            }
        }
        balls = balls.OrderBy(v => v.y).ToList();
        balls.Reverse();

        foreach (Vector2Int pos in balls)
        {
            int newOffset = FieldScript.instance.collums[pos.x].Offset;
            FieldScript.instance.collums[pos.x].DeleteBallInstant(pos.y, newOffset);
        }

        BallManagerScript.PauseGame--;
        yield break;
    }
}
