using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Класс вешается на любой игровой объект у которого имеется hp
/// </summary>
public class hp : MonoBehaviour, ihp
{
    #region Variables
 

    /// <summary>
    /// Текст на экране Крит
    /// </summary>
    public GameObject KritText;
    /// <summary>
    /// Количество жизней игрового объекта
    /// </summary>
    public int _hp = 22;
    /// <summary>
    /// наносимый урон
    /// </summary>
    private static int damage;
    /// <summary>
    /// ширина экрана
    /// </summary>
    private int w;
    /// <summary>
    /// высота экрана
    /// </summary>
    private int h;
    /// <summary>
    /// Флаг крита
    /// </summary>
    private bool krit;
    /// <summary>
    /// Расположение текста урона центрального моба
    /// </summary>
    private Vector2 vmC;
    /// <summary>
    /// Расположение текста урона  моба слева
    /// </summary>
    private Vector2 vmL;
    /// <summary>
    /// Расположение текста урона  моба справа
    /// </summary>
    private Vector2 vmR;
    //через интерфейс связываемся с компонентом damage 
    private Idamage _applyingDamage;
    Mob _mob;//ссылка на моба
    private GameObject player;
    private PlayerHelper _playerHalper;
    private int cntMob;
    private bool _miss;
    #endregion
    #region Properties
    /// <summary>
    /// Количество ХП
    /// </summary>

    public int HP
    {
        get { return _hp; }
        set { _hp = value; }
    }
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage
    { get { return damage; } }
    /// <summary>
    /// Флаг отсутствия ХП
    /// </summary>
    public bool IsDead
    {

        get
        {
            return _hp <= 0;
        }
    }
    #endregion
    void Start()
    {
        _mob = GameObject.FindObjectOfType<Mob>();//найти на сцене объект с классом Mob
        player = GameObject.FindGameObjectWithTag("Player");
        _playerHalper = player.GetComponent<PlayerHelper>();
        cntMob = PlayerPrefs.GetInt("KillMobs");
    }
    void Update()
    {
        //нет хп удалить объект на котором весит компонент
        //if (_hp <= 0)
        //{
        //    Destroy(this.gameObject);
        //}
		//нет хп удалить объект на котором весит компонент
	
    }
    /// <summary>
    /// Определение крита
    /// </summary>
    /// <param name="iq"> шанс крита</param>
    /// <returns>Крит, правда</returns>
    public bool IQShans(int iq)
    {
        int shans = Random.Range(0, 100);
        if (shans > (100 - iq))
        {
            GameObject tmp = Instantiate(KritText) as GameObject;
            tmp.transform.SetParent(GameObject.Find("CanvasUI").transform);
            tmp.transform.position = new Vector2(Screen.width / 2, Screen.height /1.5f);
            krit = true;
            Destroy(tmp, 0.9f);
        }
        else
        {
            krit = false;
        }
        return krit;
    }
    /// <summary>
    /// Метод расчета конечного урона по мобу с учетом защиты моба и шанса крита
    /// </summary>
    public void ImpactDamageOnMob(Transform effect)
    {
        if (player.GetComponent<hp>().HP <= 0)
            return;

        _applyingDamage = player.GetComponent<damage>();
        IQShans(player.GetComponent<PlayerHelper>().Stamina);
        print(_applyingDamage);
        if (transform.name== "KRABfull")
        {
            Transform Effekt1Instance = (Transform)Instantiate(effect, GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>().transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.rotation);
            Effekt1Instance.transform.position = new Vector3(Effekt1Instance.transform.position.x-1f, Effekt1Instance.transform.position.y+0.4f, Effekt1Instance.transform.position.z);
            Effekt1Instance.transform.rotation = Quaternion.FromToRotation(new Vector3(-0.5f,0.7f,0), GameObject.FindGameObjectWithTag("Centr").GetComponent<Transform>().transform.right);

        }
        else
        {
            Transform Effekt1Instance = (Transform)Instantiate(effect, this.transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.rotation);
            Effekt1Instance.transform.position = new Vector3(Effekt1Instance.transform.position.x, Effekt1Instance.transform.position.y + 0.4f, Effekt1Instance.transform.position.z);
            Effekt1Instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.forward);
        }
       
        if (!krit)
        {
            damage = _applyingDamage.ImpactDamageToMob() - GetComponent<Mob>().MobDefens;
			if (damage <= 0) 
			{
				damage = 1;
			}
        }
        else
        {
            damage = (_applyingDamage.ImpactDamageToMob() - GetComponent<Mob>().MobDefens) * 2;
			if (damage <= 0) 
			{
				damage = 1;
			}
        }
        if (damage >= this.GetComponent<hp>().HP)
        {
            if (cntMob<=100)
            {
                cntMob = PlayerPrefs.GetInt("KillMobs");
                cntMob += 1;
                PlayerPrefs.SetInt("KillMobs", cntMob);
                PlayerPrefs.Save();
                print("I Kill Mobs : " + cntMob);
                AchievmentManager.Instance.CntMob = cntMob;
            }
           
            this.GetComponent<damage>().IsGo = true;
            TextDamageToMob(this.gameObject, damage.ToString(), Color.green);//анимация смерти
            print("Dead");
            StartCoroutine(player.GetComponent<SpeedHelper>().AnimDeath(this.gameObject));
        }
        else
        {
            if (damage > 0)
            {
                _hp = _hp - damage;
                StartCoroutine(player.GetComponent<SpeedHelper>().AnimsGiveDamageMob(this.gameObject));
                player.GetComponent<PVP>().selectPlayer = 0;
                player.GetComponent<PVP>().selectTypeAtack = 0;
            }
            else
            {
                damage = 1;
                _hp = _hp - damage;
                StartCoroutine(player.GetComponent<SpeedHelper>().AnimsGiveDamageMob(this.gameObject));
                player.GetComponent<PVP>().selectPlayer = 0;
                player.GetComponent<PVP>().selectTypeAtack = 0;
            }


        }
        player.GetComponent<PVP>().selectPlayer = 0;
        player.GetComponent<PVP>().selectTypeAtack = 0;
    }
    /// <summary>
    /// Метод расчета конечного урона по игроку с учетом защиты игрока и шанса крита моба
    /// </summary>
    public void ImpactDamageOnPlayer(GameObject mob)
    {
        int missedShans = Random.Range(_playerHalper.LvlPlayer,29);
        if (missedShans>25)
        {
            _miss = true;
            print(_playerHalper.LvlPlayer);
        }
        else
        {
            _miss = false;
        }
        if (!_miss)
        {
            _applyingDamage = mob.GetComponent<damage>();//урон моба
            IQShans(mob.GetComponent<Mob>().IQ);//определение шанса крита моба
            if (!krit)
            {
                damage = _applyingDamage.ImpactDamageToPlayer() - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelper>().Stamina;
            }
            else
            {
                damage = (_applyingDamage.ImpactDamageToPlayer() - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHelper>().Stamina) * 2;
            }
            if (damage > 0)
            {

                GameObject.FindGameObjectWithTag("Player").GetComponent<hp>()._hp -= damage;
            }
            else
            {
                damage = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<hp>()._hp -= damage;
            }
        }
        else
        {
            damage =0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<hp>()._hp -= damage;
        }

    }
    /// <summary>
    /// Вывод текста урона по игроку
    /// </summary>
    /// <param name="mob">позиция моба</param>
    /// <param name="txt">текст</param>
    /// <param name="col">цвет</param>
    public void TextDamageToPlayer(GameObject mob, string txt, Color32 col)//вывод урона по игроку в виде текста
    {
		
        w = Screen.width;
        h = Screen.height;
        vmC = new Vector2(w / 2, h / 2);
        vmL = new Vector2(w / 3, h / 2);
        vmR = new Vector2(w * 2 / 3, h / 2);
        switch (mob.GetComponentInParent<Transform>().parent.name)
        {
            case "C": ImpactDamageOnPlayer(mob);if(!_miss) CombatTextManager.Instance.CreateText(vmC, damage.ToString(), col); else CombatTextManager.Instance.CreateText(vmC, "Промах", col); break;
            case "R": ImpactDamageOnPlayer(mob);if(!_miss) CombatTextManager.Instance.CreateText(vmR, damage.ToString(), col); else CombatTextManager.Instance.CreateText(vmR, "Промах", col); break;
            case "L": ImpactDamageOnPlayer(mob);if(!_miss) CombatTextManager.Instance.CreateText(vmL, damage.ToString(), col); else CombatTextManager.Instance.CreateText(vmL, "Промах", col); break;
            default:
                break;
        }
    }
    /// <summary>
    /// Вывод текста урона по мобу
    /// </summary>
    /// <param name="mob">позиция моба</param>
    /// <param name="txt">текст</param>
    /// <param name="col">цвет</param>
    public void TextDamageToMob(GameObject mob, string txt, Color32 col)//вывод урона по игроку в виде текста
    {
        w = Screen.width;
        h = Screen.height;
        vmC = new Vector2(w / 2, h * 2 / 3);
        vmL = new Vector2(w / 3, h * 2 / 3);
        vmR = new Vector2(w * 2 / 3, h * 2 / 3);
        switch (mob.GetComponentInParent<Transform>().parent.name)
        {
            case "C": CombatTextManager.Instance.CreateText(vmC, txt, col); break;
            case "R": CombatTextManager.Instance.CreateText(vmR, txt, col); break;
            case "L": CombatTextManager.Instance.CreateText(vmL, txt, col); break;
            default: break;
        }
    }
}






