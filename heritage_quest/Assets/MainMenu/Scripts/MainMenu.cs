using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public tk2dSprite button;
	
	public GameObject selectionScreen;
	
	Vector3 initialScale,
		    currentScale;
	
	float maxx,
		  maxy;
	
	bool pulse = true;
	
	// Use this for initialization
	void Start () {
		initialScale = button.transform.localScale;
		currentScale = initialScale;
		maxx = initialScale.x + 2;
		maxy = initialScale.y + 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return)){
			selectionScreen.SetActive(true);
			gameObject.SetActive(false);
		}
		
		if (pulse){
			currentScale.x = Mathf.Lerp (currentScale.x, maxx + 1, Time.deltaTime);
			currentScale.y = Mathf.Lerp (currentScale.y, maxy + 1, Time.deltaTime);
			if (Mathf.Abs (currentScale.x - maxx) < .5f){
				pulse = !pulse;
			}
		}
		else{
			currentScale.x = Mathf.Lerp (currentScale.x, initialScale.x - 1, Time.deltaTime);
			currentScale.y = Mathf.Lerp (currentScale.y, initialScale.y - 1, Time.deltaTime);
			if (Mathf.Abs (currentScale.x - initialScale.x) < .5f){
				pulse = !pulse;
			}
		}
		
		button.transform.localScale = currentScale;
	}
}
