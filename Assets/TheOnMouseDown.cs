using UnityEngine;

public class TheOnMouseDown : MonoBehaviour
{
    [SerializeField]private MobPanelVew _panel;
    void OnMouseDown()
    {
        Debug.Log($"Кликнули по {gameObject.name}!");
        _panel.ShowDescription();
    }
}
