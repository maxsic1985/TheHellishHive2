using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	
    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private Inventory _inv;
    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);

    }
    void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))//не проигрывается анимация
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;

            if (_animator.name == "CharPanel" || _animator.name == "MagicPanel")
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
                {
                    _inv = GetComponentInChildren<Inventory>();
                    _inv.IsOpen = true;
                }
            }
        }
    }
}
