using UnityEngine;
using System.Collections;

public class razrab : MonoBehaviour {
	public GameObject RZ;//панель разработ...
	public GameObject ST;///панель настроек

	// Use this for initialization

	public void Showrazrab()
	{
		if (RZ.activeSelf == false)
		{
			RZ.SetActive(true);
			ST.SetActive (false);
		

		}
		else
		{
	
			RZ.SetActive(false);
	
		}
	}

}
