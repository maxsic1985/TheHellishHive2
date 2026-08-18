using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Компонент который вешается на любого моба
/// </summary>
public class Mob : MonoBehaviour
{
    #region Variables
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
    public int MobAtack;
    /// <summary>
    /// Шанс крита моба
    /// </summary>
    public int IQ;

    EnemyHP _enemyHp;
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
