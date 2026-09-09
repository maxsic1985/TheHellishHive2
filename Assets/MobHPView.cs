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
   [SerializeField] private EnemyHP _enemyHp;
    private Color targetColor=Color.green;

    private void Start()
    {
       
        if(_enemyHp==null)  _enemyHp = GetComponentInParent<EnemyHP>();
        maxHealth = _enemyHp.HP;
        currentHealth = _enemyHp.HP;
        _orirginScale = healthText.rectTransform.localScale.x;
        _damageScale = _orirginScale+(_orirginScale + _orirginScale) / 3;
        TakeDamage(0);
        UpdateColor();
        
        
        
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