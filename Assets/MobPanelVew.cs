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
    [SerializeField] private TMP_Text _skillName;
    [SerializeField] private TMP_Text _skillDescription;
    


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowDescription()
    {
        if(gameObject==null) return;
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
            _name.text = mob._DB.MonsterName;
            _description.text = mob._DB.MonsterDescription;
            _atack.text = "Atack:" + mob.Atack.ToString();
            _defence.text = "Defence:" + mob.MobDefens.ToString();
            _speed.text = "Speed:" + mob.Speed.ToString();
            _iq.text = "IQ:" + mob.IQ.ToString();
            _skillName.text = mob._DB.MobSkill.SkillName;
            _skillDescription.text =mob._DB.MobSkill.SkillDescription;
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