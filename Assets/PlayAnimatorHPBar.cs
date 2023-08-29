using UnityEngine;
using System.Collections;

public class PlayAnimatorHPBar : MonoBehaviour {

    private PlayerHelper _player;
    private int _hpCur;
    private Animator _hpAnim;
	// Use this for initialization
	void Start () {
       _player = FindObjectOfType<PlayerHelper>();
        _hpCur = _player.HpCur;
        _hpAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
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
