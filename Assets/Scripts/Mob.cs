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
    [FormerlySerializedAs("MobAtack")] public int Atack;
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
            case MobSkillEnum.GROUP_IQ_KRIT:
                _skill = gameObject.AddComponent<Skill_group_IQ>();
                break;
            case MobSkillEnum.STUNN:
                break;
            case MobSkillEnum.MANA_BURN:
                break;
            case MobSkillEnum.NO_ESCAPE:
                break;
            case MobSkillEnum.ATTACKX5_HP_10:
                break;
            case MobSkillEnum.DEB_ATTACK5:
                break;
            case MobSkillEnum.GROUP_SPEED:
                _skill = gameObject.AddComponent<Skill_group_Speed>();
                break;
            case MobSkillEnum.GROUP_ATTACK:
                _skill = gameObject.AddComponent<Skill_group_Attack>();
                break;
            case MobSkillEnum.TARGET_POISON:
                break;
            case MobSkillEnum.ATTACKX2_HALF_HP:
                break;
            case MobSkillEnum.DAMAGE_REFLECTION:
                break;
            case MobSkillEnum.DEB_DEFENCE:
                _skill = gameObject.AddComponent<Skill_deb_Defence>();
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