using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatModul : MonoBehaviour
{
    public GameObject window;
    public InputField inputf;
    public Text log;

    public void Start()
    {
    }

    private void Update()
    {
        ActivateCheatmodule();    
    }

    public void ActivateCheatmodule()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            window.SetActive(true);
        }
    }

    public void EnterCheat()
    {
        string cheat = inputf.text;

        switch (cheat)
        {
            case "1":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[1];
                GrabberScript.instance.currentBall.CurrentColor = -1;
                break;
            case "2":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[2];
                GrabberScript.instance.currentBall.CurrentColor = -2;
                break;
            case "3":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[3];
                GrabberScript.instance.currentBall.CurrentColor = -3;
                break;
            case "4":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[4];
                GrabberScript.instance.currentBall.CurrentColor = -4;
                break;
            case "5":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[5];
                GrabberScript.instance.currentBall.CurrentColor = -5;
                break;
            case "6":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[6];
                GrabberScript.instance.currentBall.CurrentColor = -6;
                break;
            case "7":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[7];
                GrabberScript.instance.currentBall.CurrentColor = -7;
                break;
            case "8":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[8];
                GrabberScript.instance.currentBall.CurrentColor = -8;
                break;
            case "9":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[9];
                GrabberScript.instance.currentBall.CurrentColor = -9;
                break;
            case "10":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[10];
                GrabberScript.instance.currentBall.CurrentColor = -10;
                break;
            case "11":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[11];
                GrabberScript.instance.currentBall.CurrentColor = -11;
                break;
            case "12":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[12];
                GrabberScript.instance.currentBall.CurrentColor = -12;
                break;
            case "13":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[13];
                GrabberScript.instance.currentBall.CurrentColor = -13;
                break;
            case "14":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[14];
                GrabberScript.instance.currentBall.CurrentColor = -14;
                break;
            case "15":
                GrabberScript.instance.currentBall.ability = SpriteHolder.instance.abilitys[15];
                GrabberScript.instance.currentBall.CurrentColor = -15;
                break;
        }

        log.text += cheat;
        inputf.text = "";
        window.SetActive(false);
    }
}
