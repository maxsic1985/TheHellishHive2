using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPView : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool _bossIsEscape;
    private bool _bossIsDead;
    private _randomMob _rm;
    [SerializeField] private TMP_Text hpText;

    private void Start()
    {
        _rm = FindAnyObjectByType<_randomMob>();
        _bossIsDead = _rm.KillBoss > 0 ? true : false;
        gameObject.SetActive(!_bossIsDead);

        if (_rm == null) return;

        _bossIsEscape = _rm.boosIsView;
        hpText.enabled = _bossIsEscape;
    }

    private void LateUpdate()
    {
        Debug.Log("dead" + _bossIsDead);
        if (_rm.KillBoss > 0)
        {
            hpText.enabled = false;
            Destroy(gameObject);
            return;
        }

     //   Debug.Log("boos" + _rm.boosIsView);
        hpText.enabled = _rm.boosIsView;
    }
}