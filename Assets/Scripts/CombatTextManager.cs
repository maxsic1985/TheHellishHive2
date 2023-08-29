using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Создает текст уронов на сцене, мэнеджер
/// </summary>
public class CombatTextManager : MonoBehaviour
{
    #region Variables
    public static float health;
    public GameObject player;
    public GameObject textPrefab;
    public RectTransform canvasTransform;
    public float speed;
    public Vector3 direction;
    private static CombatTextManager instance;
    public float fadeTime;
    #endregion
    #region Properties
    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }

    }
    #endregion
    #region Voids
    public void CreateText(Vector2 position, string txt, Color32 col)
    {
        GameObject sct = (GameObject)Instantiate(textPrefab, position, Quaternion.identity);
        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        sct.GetComponent<Text>().text = txt;
        sct.GetComponent<Text>().color = col;
        transform.LookAt(CombatTextManager.Instance.player.transform.position);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime);
    }
    #endregion
}
