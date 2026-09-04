using System;
using UnityEngine;

public class TheOnMouseDown : MonoBehaviour
{
    [SerializeField]private MobPanelVew _panel;
    void OnMouseDown()
    {
        if(_panel==null) return;
        Debug.Log($"Кликнули по {gameObject.name}!");
        _panel.ShowDescription();
    }

    private void OnMouseUp()
    {
        if(_panel==null) return;
        _panel.ShowDescription();
    }
}
