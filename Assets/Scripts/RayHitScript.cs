using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RayHitScript : MonoBehaviour
{
	
    public Transform Luch;//для создания луча
    public GameObject Butt;//рука
    public GameObject textNoKey;
    private Ray ray;
    private RaycastHit hit;
    #region Ящики

    public GameObject box1;//ящики
    public GameObject box2;
    public GameObject box3;
    public AudioClip OpenBox;
    public AudioClip OpenBox2;
    public AudioClip OpenBox3;//звук открытия
    public AudioSource OpenBoxSource;//выбор уха
    #endregion
    #region Двери простые
    public GameObject zamok;//весь замок
    public GameObject Btndore1;//
    public GameObject Btndore2;//
	public GameObject[] ni4ka1;//
    public GameObject dore1;//
    public GameObject dore2;//
    public AudioClip OpenDoreSound;//
    public AudioSource OpenPlit;//
    public AudioSource OpenPlit2;//
    public Animator animatorPlita2;//
    #endregion
    #region Инвентари
    public Inventory chest1;//инвентари
    public Inventory chest2;
    public Inventory chest3;
    #endregion
    #region Двери с ключом
    public GameObject zomochek1;//замок с ключем
    public GameObject reshDver;
    public AudioClip Openresh;//звук открытия
    public AudioClip noKey;//звук открытия
    public Image key;
    private int keystate;
    private PlayerHelper _palyerHalper;
    #endregion
    private static RayHitScript instance;
    public static RayHitScript Instance
    {
        get
        {

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<RayHitScript>();
            }
            return instance;

        }

    }

    private void Start()
    {
        _palyerHalper = FindObjectOfType<PlayerHelper>();
         keystate = PlayerPrefs.GetInt("KeyLvl1");//показ в меню ключ, если он найден и сохранен
        if (keystate > 0)
        {
            key.enabled = true;
        }
        else
        {
            key.enabled = false;
        }
        foreach (GameObject item in ni4ka1)
        {
            if (PlayerPrefs.GetInt(item.name)==1)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }
    void Update()
    {
        ray = new Ray(Luch.position, Luch.forward);// создает луч
        if (Physics.Raycast(ray, out hit, 2))//луч падает на объект и растояние меньше 2
        {
           // print(hit.transform);
            switch (hit.transform.GetComponent<Collider>().tag)//если тэг объекта на который падает луч совпадает с тэгом в перечислении то выполнить действие
            {
                case "TresureBox": GetActionOnPressed(box1);AchievmentManager.Instance.ChestBox = true; break;//ящики, вызвать метод 
                case "TresureBox2": GetActionOnPressed(box2); AchievmentManager.Instance.ChestBox = true; break;
                case "TresureBox3": GetActionOnPressed(box3); AchievmentManager.Instance.ChestBox = true; break;
                case "stoun": GetActionOnPressed(Btndore1); break;
                case "stoun2": GetActionOnPressed(Btndore2); break;
                case "zamok": GetActionOnPressed(zomochek1); break;
			    case "ni4ka": GetActionOnPressed(ni4ka1); break;
			    case "StopZone": StopZ(); break;
			default:
				Butt.SetActive (false);
				Destroy (Butt.GetComponent<Button> ());
				break;
            }
        }
        else
        {
            if (textNoKey)//Спрятать надпись о необходимости ключа если отошли от замочной скважины
            {
                textNoKey.SetActive(false);
            }
            Butt.SetActive(false);//если отошли от объекта то спрятать руку
            Destroy(Butt.GetComponent<Button>());// если отошли от объекта то удалить функциональную кнопку с руки
            Inventory[] tempCHest = { chest1, chest2, chest3 };//Для того чтобы спрятать инвентарь если игрок отошел от сундука
            foreach (Inventory ch in tempCHest)//перебираем инвентари, находим тот который открыт и закрываем как только отошли
            {
                if (ch.IsOpen)//Если отошли от ящика и он открыт то сохранить все что переложидли в ящик
                {
                    ch.ShowInventory();
                    ch.SaveInventory();
                    PlayerPrefs.SetInt(ch.name, 1);//Флаг о том, что инвентарь ящика сохранен, для устранения ошибки при попытки загрузить не сохраненный инвентарь
                    SaveHalper.Instance.SaveInventory();//Сохранить только инвентарь
                }
            }
        }
    }
    private void GetActionOnPressed(GameObject box)//добавляет руке функционал в зависимости от объекта на который направлена
    {
        Butt.SetActive(true);
        if (!Butt.GetComponent<Button>())//Определение инвентаря для ящика
        {
            if (box.name == "tresureBoxPrefab3")//если имя такое то назначить инвентарь chest3 ....
            {
                Butt.AddComponent<Button>().onClick.AddListener(() => SendParamBox(box, chest3));
            }
            else if (box.name == "tresureBoxPrefab1")
            {
                Butt.AddComponent<Button>().onClick.AddListener(() => SendParamBox(box, chest2));
            }
            else if (box.name == "tresureBoxPrefab")
            {
                Butt.AddComponent<Button>().onClick.AddListener(() => SendParamBox(box, chest1));
            }
            else if (box.name == "KnopkaD (1)")
            {
                print(box.name);
                Butt.AddComponent<Button>().onClick.AddListener(() => OpenDore(box, dore2));
            }
            else if (box.name == "KnopkaD")
            {
                print(box.name);
                Butt.AddComponent<Button>().onClick.AddListener(() => OpenDore(box, dore1));
            }
            else if (box.name == "zamok")
            {
                print(box.name);
                Butt.AddComponent<Button>().onClick.AddListener(() => OpenDore(box, reshDver));
            }
        }
    }
    private void GetActionOnPressed(GameObject[] box)//добавляет руке функционал в зависимости от объекта на который направлена
    {
        Butt.SetActive(true);
        if (!Butt.GetComponent<Button>())//Определение инвентаря для ящика
        {
            for (int i = 0; i < box.Length; i++)
            {
                if (box[i].name == hit.transform.name)
                {
                    Butt.AddComponent<Button>().onClick.AddListener(() => GetBag());
                }
            }
          
        }
    }
    private void SendParamBox(GameObject box, Inventory chest)//управление ящиками , принимает сам объект ящика и инвентарь соответствующий этому ящику
    {
        OpenBoxSource.GetComponent<AudioSource>().PlayOneShot(OpenBox);//проиграть звук открытия ящика
        if (box.GetComponentInChildren<Transform>().GetChild(0).tag == "Untagged")//если дочерний объект ящика имеет тэг Untagged проиграть анимацию и изменить у дочернего объекта тэг на IsOpen, чтобы знать , что этот ящик открыт 
        {
            box.GetComponentInChildren<Animation>().CrossFade("box_open");
            box.GetComponentInChildren<Transform>().GetChild(0).tag = "IsOpen";
            if (!chest.IsOpen)//инвентарь закрыт
            {
                if (SaveHalper.Instance.Savining == 1)//если имеются сохранения игры, то загрузить инвентарь
                {
                    int a = PlayerPrefs.GetInt(chest.name);//получить сведения об сохранении инвентаря для выбранного ящика
                    if (a > 0)//Если ящик открывался и инвентарь в нем сохранялся 
                    {
                        chest.LoadInventory();
                    }
                }
            }

            if (box.name == "tresureBoxPrefab3")//Ключ лежит в 3 ящике
            {
                print("Найден ключ, можно проиграть анимаю как ключ перемещается из инвентаря на панель игрока");
                AchievmentManager.Instance.Key = true;
                key.enabled = true;
                if (keystate == 0)
                {
                    PlaceInBox(chest, InventoryManager.Instance.ItemCont.Consumeables[5]);
                    PlaceInBox(chest, InventoryManager.Instance.ItemCont.Consumeables[6]);
                    PlayerPrefs.SetInt("KeyLvl1", 1);
                    textNoKey.GetComponent<Text>().text = "Найден ключ";
                    textNoKey.SetActive(true);
                    keystate = 1;
                }

            }
            else if (box.name == "tresureBoxPrefab1")//если это ящик 1 то положить в него предмет по умолчанию
            {
                PlaceInBox(chest, InventoryManager.Instance.ItemCont.Consumeables[0]);
                PlaceInBox(chest, InventoryManager.Instance.ItemCont.Consumeables[4]);



            }
            else if (box.name == "tresureBoxPrefab")//если это ящик 1 то положить в него предмет по умолчанию
            {
                PlaceInBox(chest, InventoryManager.Instance.ItemCont.Consumeables[0]);
                PlaceInBox(chest, InventoryManager.Instance.ItemCont.Weapons[2]);



            }
            chest.GetComponent<Transform>().SetAsLastSibling();//открытый инвентарь ящика переместить в верхний слой в Canvas
            chest.ShowInventory();//показать инвент
            chest.SaveInventory();//сохранить инвентарь
            PlayerPrefs.SetInt(chest.name, 1);//Флаг о сохранении инвентаря для ящика
            SaveHalper.Instance.SaveInventory();//Сохранить только инвентарь
        }
        else if (box.GetComponentInChildren<Transform>().GetChild(0).tag == "IsOpen")// если ящик открыт то закрыть его
        {
            box.GetComponentInChildren<Animation>().CrossFade("box_close");
            box.GetComponentInChildren<Transform>().GetChild(0).tag = "Untagged";
            if (chest.IsOpen)
            {
                chest.SaveInventory();
                PlayerPrefs.SetInt(chest.GetComponentInParent<Transform>().name, 1);//сохранить инфу о том что этот ящик уже открывали
                chest.ShowInventory();
                PlayerPrefs.SetInt(chest.name, 1);
                SaveHalper.Instance.SaveInventory();//Сохранить только инвентарь
            }
        }


    }   
    private static void PlaceInBox(Inventory chest, Item item)//Определяет что положить в ящик по умолчанию
    {
       int isSave= PlayerPrefs.GetInt(chest.GetComponentInParent<Transform>().name);//проверить именно этот ящик уже открывали?
        if (isSave == 0)
       {
            GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
            tmp.AddComponent<ItemScript>();
            ItemScript newEqip = tmp.GetComponent<ItemScript>();
            newEqip.Item = item;
            chest.AddItem(newEqip);
        }
    }
    private void OpenDore(GameObject Btndore, GameObject dore)
    {

        if (Btndore.GetComponentInChildren<Transform>().GetChild(0).tag == "Untagged")//если дочерний объект ящика имеет тэг Untagged проиграть анимацию и изменить у дочернего объекта тэг на IsOpen, чтобы знать , что этот ящик открыт 
        {
            print(Btndore.name);
            if (Btndore.name == "KnopkaD")
            {
                OpenPlit.GetComponent<AudioSource>().PlayOneShot(OpenDoreSound);
                zamok.GetComponent<Transform>().GetComponent<Animation>().CrossFade("OpenPlita");
                Btndore.GetComponentInChildren<Transform>().GetChild(0).tag = "IsOpen";
                Btndore.GetComponent<Collider>().enabled = false;
                AchievmentManager.Instance.SecretDore=true;
            }
            else if (Btndore.name == "KnopkaD (1)")
            {
                OpenPlit2.GetComponent<AudioSource>().PlayOneShot(OpenDoreSound);
                animatorPlita2.SetBool("plit2Open", true);
                Btndore.GetComponent<Collider>().enabled = false;
            }
			else if (Btndore.name == "ni4ka")
			{
				OpenPlit2.GetComponent<AudioSource>().PlayOneShot(OpenDoreSound);
                Btndore.GetComponent<Collider>().enabled = false;
            }
            else if (Btndore.name == "zamok")
            {
                int key = PlayerPrefs.GetInt("KeyLvl1");//Проверка имеется ли ключ
                print(key);
                if (key > 0)
                {
                    OpenPlit2.GetComponent<AudioSource>().PlayOneShot(Openresh);
                    dore.GetComponent<Transform>().GetComponent<Animation>().CrossFade("open_resh");
                    Btndore.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    textNoKey.GetComponent<Text>().text = "Необходим ключ";
                    textNoKey.SetActive(true);//показать надпись о том, что необходим ключ
                    OpenPlit2.GetComponent<AudioSource>().PlayOneShot(Openresh);
                }
            }
        }
        //else if(Btndore.GetComponentInChildren<Transform>().GetChild(0).tag == "IsOpen")
        //{

        //}
    }//Управление дверьми
    private void GetBag()
    {
		if (hit.transform.tag == "ni4ka") 
		{
			print (hit.transform.name);
			PlayerPrefs.SetInt (hit.transform.name + "tmp", 1);
			hit.transform.gameObject.SetActive (false);
			_palyerHalper.GoldCur += Random.Range (90, 150);
		}  
    }
	private void StopZ()
	{
		if (hit.transform.tag == "StopZone") 
		{

			textNoKey.GetComponent<Text>().text = "Не доступно в этой версии игры";
			textNoKey.SetActive(true);
		}  
	}
   
}
