using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPView : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool _bossIsEscape;
    private bool _bossIsDead;
    private _randomMob _randomMob;
    [SerializeField] private TMP_Text hpText;

    private void Start()
    {
        _bossIsDead = AchievmentManager.Instance.KillBoss;
        gameObject.SetActive(!_bossIsDead);

        _randomMob = FindAnyObjectByType<_randomMob>();
        if (_randomMob == null) return;

        _bossIsEscape = _randomMob.boosIsView;
        hpText.enabled = _bossIsEscape;
    }

    private void LateUpdate()
    {
        Debug.Log("dead" + _bossIsDead);
        if (_bossIsDead)
        {
            gameObject.SetActive(false);
            return;
        }

        Debug.Log("boos" + _randomMob.boosIsView);
        hpText.enabled = _randomMob.boosIsView;
    }
}