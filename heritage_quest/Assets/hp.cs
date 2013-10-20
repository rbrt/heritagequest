using UnityEngine;
using System.Collections;

public class hp : MonoBehaviour {
	
	public GameObject healthbar;
	Vector3 original;
	health life;
	Vector3 current;

	// Use this for initialization
	void Start () {
		original = healthbar.transform.localScale;
		life = GameObject.FindGameObjectWithTag("Player").GetComponent<health>();
		current = original;
	}
	
	// Update is called once per frame
	void Update () {
		float target = (life.hp/500f)*original.x;
		current.x = Mathf.Lerp(current.x, target, Time.deltaTime);
		healthbar.transform.localScale = current;
	}
}
