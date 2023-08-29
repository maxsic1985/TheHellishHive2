using UnityEngine;
using System.Collections;

public class damageRandom : MonoBehaviour {


    public int minDamage;
    public int maxDamage;
	public int ishod;
	public int minDeff;
	public int maxDeff;
	public int ishodDeff;

	//public float timReloud=3f;
    

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (randomD ());
		GetComponent<Mob> ().MobAtack = GetComponent<Mob> ().MobAtack + Random.Range (minDamage,maxDamage);
		GetComponent<Mob> ().MobDefens = GetComponent<Mob> ().MobDefens + Random.Range (minDeff,maxDeff);

	}
	IEnumerator randomD()//метод печати текста по буквам
	{
		while (1 == 1) {

			yield return new WaitForSeconds (4f);

			GetComponent<Mob> ().MobDefens = ishodDeff;
			GetComponent<Mob> ().MobAtack = ishod;
			GetComponent<Mob> ().MobAtack = GetComponent<Mob> ().MobAtack + Random.Range (minDamage, maxDamage);
			GetComponent<Mob> ().MobDefens = GetComponent<Mob> ().MobDefens + Random.Range (minDeff,maxDeff);
	
			if (GetComponent<Mob> ().MobAtack >= ishod) {
				continue;
			}
	
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
