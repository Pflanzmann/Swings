using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public int value = 1;
    public int currentColor;
    int momentum;
    int startCollume;
    public bool activated = false;
    public bool done = false;
    public bool standby = false;

    Color color1 = Color.blue;
    Color color2 = Color.red;

    TextMeshPro text;
    public SpriteRenderer sprite;
    public BasisSpecialBallScript ability = null;

    public Material ballMaterial;
    public Material defaultMaterial;

    public int Momentum { get => momentum; set => momentum = value; }
    public int StartCollume { get => startCollume; set => startCollume = value; }

    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            ShowWeightValue();
        }
    }

    public int CurrentColor
    {
        get { return currentColor; }
        set
        {
            this.currentColor = value;
            SetColorOrImage();
        }
    }

    void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void SetColorOrImage()
    {
        if (currentColor > 0)
        {
            sprite.material = ballMaterial;

            sprite.sortingOrder = -2;

            color1 = SpriteHolder.instance.firstColors[currentColor];
            color2 = SpriteHolder.instance.secondColors[currentColor];

            sprite.material.SetColor("_Color1", color1);
            sprite.material.SetColor("_Color2", color2);
        }
        else
        {
            sprite.material = defaultMaterial;

            color1 = new Color(1, 1, 1, 1);
            color2 = new Color(1, 1, 1, 1);

            sprite.material.SetColor("_Color1", new Color(0, 0, 0, 0));
            sprite.material.SetColor("_Color2", new Color(0, 0, 0, 0));

            ability = SpriteHolder.instance.abilitys[-CurrentColor];
            sprite.sprite = SpriteHolder.instance.specialballsSprites[-CurrentColor];

            Value = 0;
            sprite.sortingOrder = -1;
        }
    }

    public void SetDeathParticleAndPlay()
    {
        ParticleSystem ps = StorageManagerScript.instance.GetParticle();
        ps.transform.position = transform.position;

        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
        ps.Play();
    }

    public void ShowWeightValue()
    {
        if (currentColor <= 0)
        {
            value = 0;
            text.text = "";
            return;
        }

        text.text = value.ToString();
    }

    public override string ToString()
    {
        return transform.position.ToString() + "\tvalue: " + Value + "\tcolor: " + CurrentColor;
    }
}
