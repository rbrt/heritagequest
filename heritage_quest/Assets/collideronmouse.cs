using UnityEngine;
using System.Collections;

public class collideronmouse : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
