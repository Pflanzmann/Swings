using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteHolder : MonoBehaviour
{
    public static SpriteHolder instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    [SerializeField] public Sprite[] ballSprites;
    [SerializeField] public Sprite[] balanceSprites;
    [SerializeField] public Sprite[] specialballsSprites;
    [SerializeField] public BasisSpecialBallScript[] abilitys;
    [SerializeField] public Color[] firstColors;
    [SerializeField] public Color[] secondColors;



}
