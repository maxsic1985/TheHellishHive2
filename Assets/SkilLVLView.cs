using System;
using System.Net.Mime;
using _SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkilLVLView : MonoBehaviour
{
   [SerializeField]  public DB_MobSkills Skill;
   private Text LvlText;
    void OnEnable()
    {

        SkillView.ActionUpSkillLvl += UpdateView;
        
        LvlText = GetComponentInChildren<Text>();
       
        UpdateView();     
            
    }

    private void OnDisable()
    {
        SkillView.ActionUpSkillLvl -= UpdateView;
    }

    private void UpdateView()
    {
        switch (YG2.envir.language)
        {
            case "en":
                LvlText.text = $"{Skill.CurrentSkillLVL.ToString()} level";
                break;
            case "ru":
                LvlText.text = $"{Skill.CurrentSkillLVL.ToString()} уровень";
                break;
            default:
                LvlText.text = $"{Skill.CurrentSkillLVL.ToString()} уровень";
                break;
        }
    }
}
