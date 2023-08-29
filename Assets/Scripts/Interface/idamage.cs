public interface Idamage
{
    /// <summary>
    /// Переменная урона по мобу, для передачи в hp
    /// </summary>
    /// <returns>Урон по мобу</returns>
    int ImpactDamageToMob();
    /// <summary>
    /// Переменная урона по игроку, для передачи в hp
    /// </summary>
    /// <returns>Урон по игроку</returns>
    int ImpactDamageToPlayer();
    /// <summary>
    /// Расчет урона по мобу, вызывается в ImpactDamage
    /// </summary>
    void GetDamageToMob();
    /// <summary>
    ///  Расчет урона по игроку, вызывается в ImpactDamage
    /// </summary>
    /// <param name="dmg"></param>
    void GetDamageToPlayer(int dmg);

}