using UnityEngine;
using System.Collections;

public class winner : MonoBehaviour {
	
	public bool win = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If trigger entercollides with pan, player has won the game
		if (col.gameObject.tag == "Player"){
			if (col.gameObject.GetComponent<movement>().getLocked())
				Debug.Log("congrats you won");
				win = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
