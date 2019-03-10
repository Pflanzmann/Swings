using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuUIScript : MonoBehaviour
{
    public void LoadLevelWithGameMaster(int level)
    {
        MenuScript.instance.OpenSceneWithButton(level);
    }

    public void LowQualityButton()
    {
        QualitySettings.SetQualityLevel(0, true);
        print(QualitySettings.GetQualityLevel());
    }
    
    public void HighQualityButton()
    {
        QualitySettings.SetQualityLevel(5, true);
        print(QualitySettings.GetQualityLevel());

    }


}
