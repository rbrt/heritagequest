using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {
	
	int basketCount;
	
	// Use this for initialization
	void Start () {
		basketCount = 3;
	}
	
	// Returns true when basket has been removed
	public bool DecrementBasketCount(){
		basketCount--;
		Debug.Log (basketCount);
		if (basketCount == 0){
			return true;
			basketCount--;
		}
		// Avoid duplicate basket removals or something
		else if (basketCount < 0){
			return false;
		}
		else {
			return false;
		}
	}
}
