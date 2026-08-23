using UnityEngine;

public class MobHPView : MonoBehaviour
{
    private EnemyHP _enemyHp;
    private SpriteRenderer _mobHpView;
    private float _initHP;
    private float _targetFill;
    private float _currentFill;
    private float _lerpSpeed=5f;
   
    [SerializeField] private Color _fullHealthColor = Color.green;
    [SerializeField] private Color _halfHealthColor = Color.yellow;
    [SerializeField] private Color _lowHealthColor = Color.red;
    [SerializeField] private float _lowHealthThreshold = 0.25f; // 25%

    void Start()
    {
        _enemyHp = GetComponentInParent<EnemyHP>();
        gameObject.TryGetComponent<SpriteRenderer>(out _mobHpView);
        if (_mobHpView == null || _enemyHp == null) return;

        _initHP = _enemyHp._hp;
        _currentFill = 1f;
        _targetFill = _enemyHp._hp / _initHP;
        
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (_mobHpView == null || _enemyHp == null) return;
        _targetFill = _enemyHp._hp / _initHP;
        if (!Mathf.Approximately(_targetFill, _currentFill))
        {
            _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * _lerpSpeed);
            
            // Если почти достигли цели - сразу ставим точное значение
            if (Mathf.Abs(_currentFill - _targetFill) < 0.001f)
                _currentFill = _targetFill;
                
            UpdateBar(_currentFill);
        }
        
     
        UpdateColor();
    }
    
    private void UpdateBar(float fillAmount)
    {
        if (_mobHpView == null) return;
        
        // Меняем масштаб по X (полоска растет вправо)
        Vector3 scale = _mobHpView.transform.localScale;
        scale.x = fillAmount;
        _mobHpView.transform.localScale = scale;
        
        // Если полоска должна уменьшаться от центра - меняйте pivot у спрайта
        // или используйте смещение позиции:
        // float offset = (1f - fillAmount) * 0.5f;
        // _barRenderer.transform.localPosition = new Vector3(offset, 0, 0);
    }
    
    
    // Обновление цвета в зависимости от здоровья
    private void UpdateColor()
    {
        if (_mobHpView == null) return;
        Color targetColor;
        
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
        
        _mobHpView.color = targetColor;
    }
}