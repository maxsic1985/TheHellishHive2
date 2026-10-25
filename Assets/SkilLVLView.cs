using System.Net.Mime;
using _SO;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkilLVLView : MonoBehaviour
{
   [SerializeField]  public DB_MobSkills Skill;
   private Text LvlText;
    void OnEnable()
    {
      
        
        
        LvlText = GetComponentInChildren<Text>();
       
        switch (YG2.envir.language)
        {
            case "en":
                LvlText.text =$"{Skill.CurrentSkillLVL.ToString()} level";
                break;
            case "ru":
                LvlText.text =$"{Skill.CurrentSkillLVL.ToString()} уровень";
                break;
            default:
                LvlText.text =$"{Skill.CurrentSkillLVL.ToString()} уровень";
                break;
        }     
            
    }


}
