using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Определяет движение текста урона и последующее удаление
/// Вызывается в CombatTextManager
/// </summary>
public class CombatText : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Вектор направления движения текста
    /// </summary>
    private Vector3 direction;
    /// <summary>
    /// Скорость движения текста
    /// </summary>
    private float speed;
    /// <summary>
    /// Время исчезновения текста
    /// </summary>
    private float fadeTime;
    #endregion
    #region Voids
    void Update()
    {
        //задаем движение
        float translation = speed * Time.deltaTime;
        transform.Translate(direction * translation);
    }
    /// <summary>
    /// Инициализация движения текста
    /// </summary>
    /// <param name="speed">скорость</param>
    /// <param name="direction">нправление</param>
    /// <param name="fadeTime">время существования</param>
    public void Initialize(float speed, Vector3 direction, float fadeTime)
    {
        this.speed = speed;
        this.fadeTime = fadeTime;
        this.direction = direction;
        StartCoroutine(FadeOUT());
    }
    /// <summary>
    /// Плавное исчезновение текста коротин
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOUT()
    {
        float startAlpha = GetComponent<Text>().color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;
        while (progress < 1.0)
        {
            Color tmpColor = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    #endregion
}
