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
            SetPoison();
            return;
        }
        else if (_speedHelper.EndRound1 == false)
            _usedPoison = false;
    }

    public void Init(int poison)
    {
        _powerPoison = poison;
    }

    public void SetPoison()
    {
        PlayerHelper.Instance.HpCur -= _powerPoison;
        _usedPoison = true;
        CombatTextManager.Instance.CreateText(new Vector2(Screen.width / 2f, Screen.height / 2f),
            _powerPoison.ToString(), Color.magenta);
    }
}