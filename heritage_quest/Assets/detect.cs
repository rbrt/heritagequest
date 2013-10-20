using UnityEngine;
using System.Collections;

public class detect : MonoBehaviour {
	public string thoughtSprite;
	public GameObject thought;
	bool hit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If trigger entercollides with player, change thought
		if (col.gameObject.tag == "Player"){
			hit = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hit){
			var sprite = thought.gameObject.GetComponent<tk2dSprite>();
			sprite.SetSprite(thoughtSprite);
		}
	}
}
