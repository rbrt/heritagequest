using UnityEngine;
using System.Collections;

public class HItbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		Debug.Log (other.name);
		
	}
}
