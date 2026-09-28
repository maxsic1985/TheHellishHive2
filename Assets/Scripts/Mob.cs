using UnityEngine;
using System.Collections;
using _SO;
using Skills;
using UnityEngine.Serialization;
using UnityEngine.UI;
/// <summary>
/// Компонент который вешается на любого моба
/// </summary>
public class Mob : MonoBehaviour
{
    
    #region Variables

    public DB_Mobs _DB;
    
    /// <summary>
    /// награда выпадающая с моба
    /// </summary>
    public GameObject PriceForMob;
    /// <summary>
    /// Скорость моба, определение очередности  ходов
    /// </summary>
    public int speed;
    /// <summary>
    /// Опыт за моба
    /// </summary>
    public int expForMob;
    /// <summary>
    /// Голда за моба
    /// </summary>
    public int mobForGold;
    /// <summary>
    /// Защита моба
    /// </summary>
    public int MobDefens;
    /// <summary>
    /// Атака моба
    /// </summary>
    public int Atack;
    /// <summary>
    /// Шанс крита моба
    /// </summary>
    public int IQ;

    EnemyHP _enemyHp;
    private ISkill _skill;
    #endregion
    #region Properties
    /// <summary>
    /// Скорость моба
    /// </summary>
    public int Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    /// <summary>
    /// Опыт за моба
    /// </summary>
    public int ExpForMob
    {
        get { return expForMob; }
        set { expForMob = value; }
    }
    #endregion
    #region Voids
    void Start()
    {
        _enemyHp = GetComponent<EnemyHP>();
        switch (_DB.MobSkill.Skill)
        {
            case SkillEnum.GROUP_IQ_KRIT:
                _skill = gameObject.AddComponent<Skill_group_IQ>();
                break;
            case SkillEnum.STUNN:
                _skill = gameObject.AddComponent<Skill_Stunn>();
                break;
            case SkillEnum.MANA_BURN:
                _skill = gameObject.AddComponent<Skill_ManaBurn>();
                break;
            case SkillEnum.NO_ESCAPE:
                _skill = gameObject.AddComponent<Skill_NoESC>();
                break;
            case SkillEnum.ATTACKX5_HP_10:
                _skill = gameObject.AddComponent<Skill_AttackX5>();
                break;
            case SkillEnum.DEB_ATTACK5:
                _skill = gameObject.AddComponent<Skill_deb_Attack>();
                break;
            case SkillEnum.GROUP_SPEED:
                _skill = gameObject.AddComponent<Skill_group_Speed>();
                break;
            case SkillEnum.GROUP_ATTACK:
                _skill = gameObject.AddComponent<Skill_group_Attack>();
                break;
            case SkillEnum.TARGET_POISON:
                _skill = gameObject.AddComponent<Skill_Poison>();  
                break;
            case SkillEnum.ATTACKX2_HALF_HP:
                _skill = gameObject.AddComponent<Skill_AttackX2>();
                break;
            case SkillEnum.DAMAGE_REFLECTION:
                _skill = gameObject.AddComponent<Skill_DamageREflection>();
                break;
            case SkillEnum.DEB_DEFENCE:
                _skill = gameObject.AddComponent<Skill_deb_Defence>();
                break;
            case SkillEnum.IQ_X2_HALF_HP:
                _skill = gameObject.AddComponent<Skill_IQ_kX2>();
                break;
        }
        
    }
    void Update()
    {
        //убить моба если хп меньше нуля
        if (_enemyHp.HP <= 0)
        {
            Destroy(this.gameObject,1);
        }

    }
    #endregion
}