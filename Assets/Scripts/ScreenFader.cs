using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Затемнение экрана при перемещении в комнату боя
/// </summary>
public class ScreenFader : MonoBehaviour
{

    public Image image;
    public bool fadeering;
    private static Color faderColor;
    private static float maxTime, curTime;
    private static bool isColor, isClear, fade=false;

    public static void Fader(float time, Color color)
    {
        if (curTime != 0) return;
        isClear = false;
        isColor = false;
        fade = !fade;
        faderColor = color;
        faderColor.a = 1;
        maxTime = time;
    }

    //public static void Fader(float time)
    //{
    //    if (curTime != 0) return;
    //    isClear = false;
    //    isColor = false;
    //    fade = !fade;
    //    faderColor.a = 1;
    //    maxTime = time;
    //}

    void Awake()
    {
        
      fade = false;
        faderColor = Color.black; // цвет по умолчанию
        image.color = Color.black; // цвет на старте
    }

    void SetClear()
    {
        if (isClear) return;
        faderColor.a = 1 - GetValue();
        image.color = faderColor;
        if (image.color.a <= 0)
        {
            image.color = faderColor;
            isClear = true;
            curTime = 0;
        }
    }

    void SetColor()
    {
        if (isColor) return;
        faderColor.a = GetValue();
        image.color = faderColor;
        if (image.color.a >= 1)
        {
            image.color = faderColor;
            isColor = true;
            curTime = 0;
        }
        fade = !fade;
    }

    float GetValue()
    {
        float t = 0;
        curTime += Time.deltaTime;
        t = curTime / maxTime;
        return t;
    }

    void Update()
    {
        fadeering = fade;
        if (fade) SetColor(); else SetClear();
    }
  
    public static bool screenColor // true, когда экран полностью закрашен
    {
        get { return isColor; }
    }

    public static bool screenClear // true, когда экран чистый
    {
        get { return isClear; }
    }
}