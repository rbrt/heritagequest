using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	public tk2dSprite burntToast,
					  basketsBack;
	
	GameObject currentSelection;
	
	Vector3[] initialScale,
		      currentScale;
	
	float maxx,
		  maxy,
		  modifier = .01f,
		  lastChangeTime = 0,
	 	  cycleDelay = .3f;
	
	bool pulse = true;
	
	int choice = 0;
	
	// Use this for initialization
	void Start () {
		currentSelection = basketsBack.gameObject;
		initialScale = new Vector3[2];
		currentScale = new Vector3[2];
		
		initialScale[0] = basketsBack.gameObject.transform.localScale;
		currentScale[0] = initialScale[0];
		
		initialScale[1] = burntToast.gameObject.transform.localScale;
		currentScale[1] = initialScale[1];
		
		maxx = initialScale[0].x + .02f;
		maxy = initialScale[0].y + .02f;
		
		
	}
	
	void ChangeSelection(){
		if (Time.time - lastChangeTime > cycleDelay){
			lastChangeTime = Time.time;
			if (choice == 0){
				basketsBack.transform.localScale = initialScale[0];
				StartCoroutine(ReturnToStartValue(currentSelection.gameObject, initialScale[0]));
				currentSelection = burntToast.gameObject;
				choice = 1;
			}
			else{
				burntToast.transform.localScale = initialScale[1];
				StartCoroutine(ReturnToStartValue(currentSelection.gameObject, initialScale[1]));
				currentSelection = basketsBack.gameObject;
				choice = 0;
			}
		}
		Debug.Log (currentSelection);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)){
			ChangeSelection();
		}
		else if (Input.GetKeyDown(KeyCode.Return)){
			if (currentSelection == basketsBack.gameObject){
				Application.LoadLevel("basketsback");
			}
			else{
				Debug.Log ("Not yet implemented");
			}
		}
		
		if (pulse){
			currentScale[choice].x = Mathf.Lerp (currentScale[choice].x, maxx + modifier, Time.deltaTime / 2);
			currentScale[choice].y = Mathf.Lerp (currentScale[choice].y, maxy + modifier, Time.deltaTime / 2);
			if (Mathf.Abs (currentScale[choice].x - maxx) < .01f){
				pulse = !pulse;
			}
		}
		else{
			currentScale[choice].x = Mathf.Lerp (currentScale[choice].x, initialScale[choice].x - modifier, Time.deltaTime / 2);
			currentScale[choice].y = Mathf.Lerp (currentScale[choice].y, initialScale[choice].y - modifier, Time.deltaTime / 2);
			if (Mathf.Abs (currentScale[choice].x - initialScale[choice].x) < .005f){
				pulse = !pulse;
			}
		}
		
		currentSelection.transform.localScale = currentScale[choice];
	}
	
	IEnumerator ReturnToStartValue(GameObject text, Vector3 initial){
		bool returning = true;
		Vector3 current = text.transform.localScale;
		
		while (returning){
			float newx = Mathf.Lerp (current.x, initial.x - modifier, Time.deltaTime / 2);
			float newy = Mathf.Lerp (current.y, initial.y - modifier, Time.deltaTime / 2);
			
			if (Mathf.Abs (current.x - initial.x) < .01f){
				current = initial;
				returning = false;
			}
			
			text.transform.localScale = current;
			
			yield return null;
		}
		
	}
}
