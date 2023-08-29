using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start ()

    {
        yield return new WaitForSeconds(1);
        GetComponent<DialogBubble>().ShowBubble(this.gameObject.GetComponent<DialogBubble>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
