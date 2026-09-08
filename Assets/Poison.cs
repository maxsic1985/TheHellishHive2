using System;
using DG.Tweening;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private SpeedHelper _speedHelper;
    private _randomMob _rm;
    private int _powerPoison;
    private bool _usedPoison;

    void Start()
    {
        _speedHelper = GetComponent<SpeedHelper>();
        _rm = GetComponent<_randomMob>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerHelper.Instance._hasPoison || _rm.EnbBattle) return;
        if (_speedHelper.EndRound1 && _usedPoison == false)
        {
            SetDamagePoison();
            return;
        }
        else if (_speedHelper.EndRound1 == false)
            _usedPoison = false;
    }

    public void Init(int poison)
    {
        _powerPoison = poison;
    }

    public void SetDamagePoison()
    {
        PlayerHelper.Instance.HpCur -= _powerPoison;
        _usedPoison = true;
        // CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2f, Screen.height / 4f),
        //     _powerPoison.ToString(), Color.gray);


        var hpText = PlayerHelper.Instance.HPtextBAR;
        hpText.color = Color.green;
       
        Sequence pulseSequence = DOTween.Sequence();
        pulseSequence.Append(PlayerHelper.Instance.HPtextBAR.transform.DOScale(1.5f, 0.5f));
        pulseSequence.Append(PlayerHelper.Instance.HPtextBAR.transform.DOScale(1f, 0.5f));
        pulseSequence.Play();
    }

    public void OnDestroy()
    {
        PlayerHelper.Instance.HPtextBAR.color = Color.white;
    }
}