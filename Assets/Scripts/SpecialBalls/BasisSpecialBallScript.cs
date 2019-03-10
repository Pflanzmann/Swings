using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasisSpecialBallScript : ScriptableObject
{
    public abstract IEnumerator CAbility(int collume, int row, int offset);
}
