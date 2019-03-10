using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilitys/SpawnSuiter")]
public class SpawnSuiter : BasisSpecialBallScript
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

        if (color > 0)
            for (int i = 0; i < 8; i++)
            {
                SpawnerScript.instance.ChangeBall(i, 0, color);
                SpawnerScript.instance.ChangeBall(i, 1, color);
            }

        FieldScript.instance.collums[collume].DeleteBallInstant(row, offset);

        BallManagerScript.PauseGame--;
        yield break;
    }
}
