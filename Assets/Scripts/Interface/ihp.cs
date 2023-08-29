using UnityEngine;
public interface ihp
{
    /// <summary>
    /// Количество ХП
    /// </summary>
    int HP { get; }
    /// <summary>
    /// Флаг отсутствия ХП
    /// </summary>
    bool IsDead { get; }
    /// <summary>
    /// Метод расчета конечного урона по мобу с учетом защиты моба и шанса крита
    /// </summary>
    void ImpactDamageOnMob(Transform errect);
    /// <summary>
    /// Метод расчета конечного урона по игроку с учетом защиты игрока и шанса крита моба
    /// </summary>
    void ImpactDamageOnPlayer(GameObject m);

}
