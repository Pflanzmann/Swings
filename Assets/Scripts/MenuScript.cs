using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;

    [SerializeField] Image blackImage;

    public HighscoreTable table;

    int currentLevel = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadHighscores();
        StartCoroutine(FadeScreenIn());

    }

    public void LoadHighscores()
    {
        if (table == null)
        {
            table = SaveSystemScript.LoadScore();
        }
        if (table == null)
        {
            table = new HighscoreTable();
            SaveSystemScript.SaveTable(table);
        }
    }

    public void SaveHighscore()
    {
        SaveSystemScript.SaveTable(table);
    }

    public void OpenSceneWithButton(int level)
    {
        StartCoroutine(FadeScreenOut(level));
    }

    public IEnumerator FadeScreenIn()
    {
        float alpha = 1.1f;
        while (alpha >= 0)
        {
            alpha -= Time.deltaTime * 0.5f;
            blackImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        yield break;
    }

    public IEnumerator FadeScreenOut(int level)
    {
        float alpha = 0f;
        while (alpha <= 1)
        {
            alpha += Time.deltaTime * 0.6f;
            blackImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        SceneManager.LoadScene(level);
        yield break;
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(FadeScreenIn());
    }
}
