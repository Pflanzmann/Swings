using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderScript : MonoBehaviour
{
    public Material mat;
    Color color1 = Color.red;
    Color color2 = Color.blue;
    SpriteRenderer sr;
    public int value = 0;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(value == 1)
        {
            sr.material.SetColor("_Color1", color1);
            sr.material.SetColor("_Color2", color2);

            sr.material.SetTextureOffset("_NoiseTex", new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f)));

            sr.material.SetFloat("_OffsetX", Random.Range(0f, 100f));
            sr.material.SetFloat("_OffsetY", Random.Range(0f, 100f));

        }

    }
}
