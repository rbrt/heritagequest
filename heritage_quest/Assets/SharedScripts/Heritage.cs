using UnityEngine;
using System.Collections;

public class Heritage : MonoBehaviour {
	
	public GameObject innerPanel;
	
	Vector3 currentScale;
	
	bool ready = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ready){
			transform.localScale = currentScale;
		}
	}
	
	public void SetInnerPanel(GameObject panel){
		innerPanel = panel;
		panel.transform.parent = transform;
		
		innerPanel.transform.localPosition = new Vector3(0, -.038f, -1);
		
		GetComponent<MeshRenderer>().enabled = true;
		GameObject root = Camera.main.gameObject;
		foreach (var node in root.GetComponentsInChildren<Transform>()){
			if (node.tag != "MainCamera"){
				node.gameObject.SetActive(false);
			}
		}
		
		StartCoroutine(PanBackCoroutine());
	}
	
	IEnumerator PanBackCoroutine(){
		bool pan = true;
		Vector3 originalScale = transform.localScale,
				targetScale = new Vector3 (49, 37, 1);
		
		float targetx = 49,
			  targety = 37;
		
		currentScale = originalScale;
		ready = true;
		
		while(currentScale.x > targetScale.x + 1){
			float newx = Mathf.Lerp (currentScale.x, targetx, Time.deltaTime);
			float newy = Mathf.Lerp (currentScale.y, targety, Time.deltaTime);
			
			currentScale = new Vector3(newx, newy, currentScale.z);
			yield return null;
		}
		
		//currentScale = targetScale;
	}
}
