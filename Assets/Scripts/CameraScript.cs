using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private bool landscape = true;

    public GameObject ui;
    public GameObject uI16_9;
    public GameObject uI9_16;

    float currentAspect = 1;
    Camera mainCamera;

    RectTransform[] rtaUI;
    RectTransform[] rta1;
    RectTransform[] rta2;

    void Start()
    {
        rtaUI = ui.GetComponentsInChildren<RectTransform>();
        rta1 = uI16_9.GetComponentsInChildren<RectTransform>();
        rta2 = uI9_16.GetComponentsInChildren<RectTransform>();

        mainCamera = Camera.main;

        double size;

        float ratio = mainCamera.aspect;
        currentAspect = ratio;

        if (ratio >= 1.777777778f)
        {
            float h = Screen.currentResolution.height;
            float w = Screen.currentResolution.width;

            ratio = w / h;

            size = 12 / ratio;

            Camera.main.orthographicSize = (float)size;
            return;
        }

        size = 12 / ratio;
        landscape = true;

        if (size >= 15)
        {
            size = 8.26666667 / ratio;

            landscape = false;
        }

        Camera.main.orthographicSize = (float)size;
        SwapTransforms();
        return;
    }


    void FixedUpdate()
    {
        if (currentAspect != mainCamera.aspect)
        {
            double size;

            float ratio = mainCamera.aspect;
            currentAspect = ratio;
            landscape = true;

            if (ratio >= 1.777777778f)
            {
                float h = Screen.currentResolution.height;
                float w = Screen.currentResolution.width;

                ratio = w / h;

                size = 12 / ratio;

                Camera.main.orthographicSize = (float)size;
                SwapTransforms();
                return;
            }

            size = 12 / ratio;

            if (size >= 15)
            {
                size = 8.26666667 / ratio;

                landscape = false;
            }

            Camera.main.orthographicSize = (float)size;
            SwapTransforms();
            return;
        }
    }


    public void SwapTransforms()
    {
        if (!landscape)
        {
            for (int i = 0; i < rtaUI.Length; i++)
            {
                rtaUI[i].localPosition = rta2[i].localPosition;
                rtaUI[i].rotation = rta2[i].rotation;
            }
        }
        else
        {
            for (int i = 0; i < rtaUI.Length; i++)
            {
                rtaUI[i].localPosition = rta1[i].localPosition;
                rtaUI[i].rotation = rta1[i].rotation;
            }
        }
    }
}
