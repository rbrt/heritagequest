using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = gameObject.transform.position.z;
		gameObject.transform.position = pos;
		Debug.Log (Camera.main.ScreenToWorldPoint(pos));
	}
}
