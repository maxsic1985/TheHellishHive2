using UnityEngine;
using System.Collections;

public class PlayAnimatorHPBar : MonoBehaviour
{
   
    private Animator _hpAnim;

    // Use this for initialization
    void Start()
    {
        _hpAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //  PlayAnimHPBAr();
    }

    public void PlayAnimHPBAr()
    {
        _hpAnim.SetBool("HPBarDamage", true);
    }

    public void StopAnims()
    {
        _hpAnim.SetBool("HPBarDamage", false);
    }
}