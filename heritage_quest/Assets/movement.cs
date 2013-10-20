using UnityEngine;
using System.Collections;
using System;

public class movement : MonoBehaviour {
	
	//public float force = 100f;
	public float speed = 0.5f;
	public bool canPick = false;
	public bool locked = false;  //boolean if seizurebrainbit is locked onto player
	GameObject currentKey;	//holds current seizure brain bit
	
	bool inBrain = false;
	
	// Use this for initialization
	void Start () {
		//Screen.showCursor = false;
	}
	
	public bool getLocked(){
		return locked;
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If player entercollides with wall, change block to hurtblock
		var sprite = col.gameObject.GetComponent<tk2dSprite>();
		if (col.gameObject.tag == "corner"){
			sprite.SetSprite("hurttoprightcorner");
			inBrain = true;
		}
		if (col.gameObject.tag == "straight"){
			sprite.SetSprite("hurtvstraight");
			inBrain = true;
		}
		if (col.gameObject.tag == "end"){
			sprite.SetSprite("hurtupend");
			inBrain = true;
		}
		if (col.gameObject.tag == "T"){
			sprite.SetSprite("hurtupT");
			inBrain = true;
		}
		
		//If player entercollides with seizurebrainbit, then canPick = true
		if (col.gameObject.tag == "key"){
			canPick = true;
			currentKey = col.gameObject;
		}
	}
	void OnCollisionExit(Collision col){	
		//If player exitcollides with wall, change hurtblock back to block
		var sprite = col.gameObject.GetComponent<tk2dSprite>();
		if (col.gameObject.tag == "corner"){
			sprite.SetSprite("toprightcorner");
			inBrain = true;
		}
		if (col.gameObject.tag == "straight"){
			sprite.SetSprite("vstraight");
			inBrain = true;
		}
		if (col.gameObject.tag == "end"){
			sprite.SetSprite("upend");
			inBrain = true;
		}
		if (col.gameObject.tag == "T"){
			sprite.SetSprite("upT");
			inBrain = true;
		}
		//If player exitcollides with seizurebrainbit, then canPick = false
		if (col.gameObject.tag == "key"){
			canPick = false;
			currentKey = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Player position follows the mouse cursor
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		bool doit = false;
		RaycastHit hit;
		Debug.DrawRay(transform.position, (pos - transform.position).normalized);
		if (Physics.Raycast(transform.position, (pos - transform.position).normalized, out hit, 100)){
			if (hit.collider.name == "goodtimes" || hit.collider.name == "pan" || hit.collider.name == "trigger" || hit.collider.name == "trigger_burnt" || hit.collider.name == "brain"){
				doit = true;
			}
			Debug.Log (hit.collider.name);
		}
		
		if ( doit){
			pos.z = gameObject.transform.position.z;
			Vector3 playerPos = gameObject.transform.position;
			playerPos.z = pos.z;
		
		//gameObject.transform.position = pos;
		//var diff = pos-transform.position;
		//rigidbody.AddForce(diff.normalized*force);
		//rigidbody.MovePosition(pos);
			gameObject.transform.position = Vector3.MoveTowards(playerPos, pos, speed);
		//Debug.Log (Camera.main.ScreenToWorldPoint(pos));
		}
		//If player clicks on seizurebrainbit then its position gets locked to player's position
		if (canPick)
			if (Input.GetMouseButtonDown(0))
				locked = true;
		else if (locked)
			if (Input.GetMouseButtonDown(0))
				locked = false;
		
		//Debug.Log (canPick + " " + locked);
			
		if (locked){
			Vector3 tempPos = pos;
			try{
				tempPos.z = currentKey.transform.position.z;
			}
			catch (NullReferenceException e){
				Debug.Log ("Dont care");
			}
			if (currentKey != null)
				currentKey.transform.position = tempPos;
		}
	}
}
