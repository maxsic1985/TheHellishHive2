using System;
using TMPro;
using UnityEngine;

public class MobPanelVew : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _atack;
    [SerializeField] private TMP_Text _defence;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _iq;


    [SerializeField] private String _mobName;
    [SerializeField] private String _descript;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowDescription()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }


        gameObject.SetActive(true);
        try
        {
            var mob = GetComponentInParent<Mob>();
            var hp = GetComponentInParent<EnemyHP>();

            _hp.text = "HP:" + hp._hp.ToString();
            _name.text = _mobName;
            _description.text = _descript;
            _atack.text = "Atack:" + mob.MobAtack.ToString();
            _defence.text = "Defence:" + mob.MobDefens.ToString();
            _speed.text = "Speed:" + mob.Speed.ToString();
            _iq.text = "IQ:" + mob.IQ.ToString();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString()+"mob or EnemyHP Not found");
            throw;
        }
       
    }
    
    void OnMouseDown()
    {
        Debug.Log($"Кликнули по {gameObject.name}!");
        ShowDescription();
    }
}