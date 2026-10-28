using System;
using _SO;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkillView : MonoBehaviour
{
    public DB_MobSkills Skill;

    public Image Image;

    public Text SkillName;
    public Text SkillDescription;
    public Text SkillMana;
    public Text SkillPower;
    public Text SkillMaxLvl;
    public Text SkillCostUp;
    public Button UpSkillBtn;


    private string manaTxt;
    private string powerTxt;
    private string maxLvlTxt;


    private void OnEnable()
    {
        UpdateView();
        Image.sprite = Skill.SkillSprite;

      
        if (UpSkillBtn != null)
        {
            UpSkillBtn.onClick.AddListener(UpLvl);
            UpdateView();
        }
    }

    private void UpLvl()
    {
        Skill.UpLvl();
        UpdateView();
    }

    private void UpdateView()
    {
       
        switch (YG2.envir.language)
        {
            case "en":
                manaTxt = "Mana:";
                powerTxt = "Power:";
                maxLvlTxt = "Level:";
                break;
            case "ru":
                manaTxt = "Мана:";
                powerTxt = "Сила:";
                maxLvlTxt = "Уровень:";
                break;
            default:
                manaTxt = "Mana:";
                powerTxt = "Power:";
                maxLvlTxt = "Level:";
                break;
        }
        
        SkillName.text = Skill.SkillName;
        SkillDescription.text = Skill.SkillDescription;
        SkillMana.text = $"{manaTxt} {Skill.Mana.ToString()}";
        SkillPower.text = $"{powerTxt} {Skill.Power.ToString()}";
        SkillMaxLvl.text = $"{maxLvlTxt}{Skill.CurrentSkillLVL.ToString()}";
        SkillCostUp.text = $"{Skill.UpLvlCost.ToString()}";
    }
}