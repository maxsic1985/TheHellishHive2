// using TMPro;
// using UnityEngine;
//
// public class MobHPView : MonoBehaviour
// {
//     private EnemyHP _enemyHp;
//     private float _initHP;
//     private float _targetFill;
//     private float _currentFill;
//     private float _lerpSpeed = 5f;
//
//     [SerializeField] TMP_Text _mobHpView;
//     [SerializeField] private Color _fullHealthColor = Color.green;
//     [SerializeField] private Color _halfHealthColor = Color.yellow;
//     [SerializeField] private Color _lowHealthColor = Color.red;
//     [SerializeField] private float _lowHealthThreshold = 0.25f; // 25%
//
//     void Start()
//     {
//         _enemyHp = GetComponentInParent<EnemyHP>();
//         gameObject.TryGetComponent<TMP_Text>(out _mobHpView);
//         if (_mobHpView == null || _enemyHp == null) return;
//
//         _initHP = _enemyHp._hp;
//         _currentFill = 1f;
//         _targetFill = _enemyHp._hp / _initHP;
//
//         UpdateColor();
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (_mobHpView == null || _enemyHp == null) return;
//         _targetFill = _enemyHp._hp / _initHP;
//         if (!Mathf.Approximately(_targetFill, _currentFill))
//         {
//             _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * _lerpSpeed);
//
//             // Если почти достигли цели - сразу ставим точное значение
//             if (Mathf.Abs(_currentFill - _targetFill) < 0.001f)
//                 _currentFill = _targetFill;
//
//             UpdateBar(_currentFill);
//         }
//
//
//         UpdateColor();
//     }
//
//     private void UpdateBar(float fillAmount)
//     {
//         if (_mobHpView == null) return;
//         _mobHpView.text = $"{fillAmount.ToString()}/{_initHP.ToString()}";
//     }
//
//
//     // Обновление цвета в зависимости от здоровья
//     private void UpdateColor()
//     {
//         if (_mobHpView == null) return;
//         Color targetColor;
//
//         if (_targetFill <= _lowHealthThreshold)
//             targetColor = _lowHealthColor;
//         else if (_targetFill < 0.5f)
//         {
//             // Плавный переход между желтым и красным
//             float t = (_targetFill - _lowHealthThreshold) / (0.5f - _lowHealthThreshold);
//             targetColor = Color.Lerp(_lowHealthColor, _halfHealthColor, t);
//         }
//         else
//         {
//             // Плавный переход между зеленым и желтым
//             float t = (_targetFill - 0.5f) / 0.5f;
//             targetColor = Color.Lerp(_halfHealthColor, _fullHealthColor, t);
//         }
//
//         _mobHpView.color = targetColor;
//     }
// }

using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MobHPView : MonoBehaviour
{
    public TMP_Text healthText;
    public float duration = 0.8f;
    [SerializeField] private float _lowHealthThreshold = 0.25f; // 25%
    [SerializeField] private Color _fullHealthColor = Color.green;
    [SerializeField] private Color _halfHealthColor = Color.yellow;
    [SerializeField] private Color _lowHealthColor = Color.red;
    private float _orirginScale;
    private float _damageScale;
    
    
    private float currentHealth = 100f;
    private float maxHealth = 100f;
    private EnemyHP _enemyHp;
    private Color targetColor=Color.white;

    private void Start()
    {
        _enemyHp = GetComponentInParent<EnemyHP>();
        maxHealth = _enemyHp.HP;
        currentHealth = _enemyHp.HP;
        _orirginScale = healthText.rectTransform.localScale.x;
        _damageScale = _orirginScale + (Mathf.Round(_orirginScale / 4));
    }


    void Update()
    {
        if (healthText == null || _enemyHp == null) return;
        //if (_enemyHp.HP <= 0)
            if (!Mathf.Approximately(_enemyHp.HP, currentHealth))
            {
                UpdateColor();
                TakeDamage(currentHealth - _enemyHp.HP);
            }
        
    }


    public void TakeDamage(float damage)
    {
        float oldHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(healthText.transform.DOScale(_damageScale, 0.1f));
        sequence.Join(healthText.DOColor(Color.red, 0.1f));

        sequence.Append(DOTween.To(() => oldHealth,
            x => healthText.text =  $"{Mathf.Round(x).ToString()}/{maxHealth.ToString()}",
            currentHealth,
            duration));

        sequence.Join(healthText.DOColor(targetColor, duration));

        sequence.Append(healthText.transform.DOScale(_orirginScale, 0.3f)
            .SetEase(Ease.OutBack));

        sequence.Play();
        
    }

    public void Heal(float amount)
    {
        float oldHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(healthText.transform.DOScale(_damageScale, 0.15f));
        sequence.Join(healthText.DOColor(Color.green, 0.15f));

        sequence.Append(DOTween.To(() => oldHealth,
            x => healthText.text = Mathf.RoundToInt(x).ToString(),
            currentHealth,
            duration));

        sequence.Join(healthText.DOColor(Color.white, duration));

        sequence.Append(healthText.transform.DOScale(_orirginScale, 0.2f)
            .SetEase(Ease.OutCubic));

        sequence.Play();
    }
    
    private void UpdateColor()
     {
         if (healthText == null) return;
         float _targetFill = _enemyHp._hp / maxHealth;

         if (_targetFill <= _lowHealthThreshold)
             targetColor = _lowHealthColor;
         else if (_targetFill < 0.5f)
         {
             // Плавный переход между желтым и красным
             float t = (_targetFill - _lowHealthThreshold) / (0.5f - _lowHealthThreshold);
             targetColor = Color.Lerp(_lowHealthColor, _halfHealthColor, t);
         }
         else
         {
             // Плавный переход между зеленым и желтым
             float t = (_targetFill - 0.5f) / 0.5f;
             targetColor = Color.Lerp(_halfHealthColor, _fullHealthColor, t);
         }
     }
    
    
}