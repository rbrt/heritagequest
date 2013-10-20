using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	
	bool ishurt = false;
	bool gameOver = false;
	public int hp = 500;

	// Use this for initialization
	void Start () {
	
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If player entercollides with wall, health starts to deplete
		if (col.gameObject.tag == "corner" || col.gameObject.tag == "T" || col.gameObject.tag == "end" || col.gameObject.tag == "straight" ){
			ishurt = true;
		}
	}
	void OnCollisionExit(Collision col){	
		//If player exitcollides with wall, change hurtblock back to block
		if (col.gameObject.tag == "corner" || col.gameObject.tag == "T" || col.gameObject.tag == "end" || col.gameObject.tag == "straight"){
			ishurt = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (ishurt){
			if (hp == 0)
				gameOver = true;
			else
				hp -= 1;
		}
	}
}
