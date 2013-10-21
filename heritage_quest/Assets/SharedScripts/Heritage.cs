using UnityEngine;
using System.Collections;
using System;

public class Heritage : MonoBehaviour {
	
	public GameObject innerPanel,
					  win,
					  lose;
	
	public AudioClip winClip,
					 loseClip;
	
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
		
		if (Input.GetKey(KeyCode.Return)){
			Application.LoadLevel("Menu");
		}
	}
	
	public void SetInnerPanel(GameObject panel, bool winner){
		innerPanel = panel;
		panel.transform.parent = transform;
		
		try{
			GameObject.FindGameObjectWithTag("backgroundmusic").GetComponent<AudioSource>().Stop();
		}
		catch (NullReferenceException){
			
		}
		
		var audio = GetComponent<AudioSource>();
		
		if (winner){
			audio.clip = winClip;
		}
		else{
			audio.clip = winClip;
		}
		audio.Play();
		
		
		innerPanel.transform.localPosition = new Vector3(0, -.038f, -1);
		
		GetComponent<MeshRenderer>().enabled = true;
		if (gameObject.name == "HeritageBox2"){
			foreach (var child in GetComponentsInChildren<MeshRenderer>()){
				child.enabled = true;
			}
		}
		
		GameObject root = Camera.main.gameObject;
		foreach (var node in root.GetComponentsInChildren<Transform>()){
			if (node.tag != "MainCamera"){
				node.gameObject.SetActive(false);
			}
		}
		
		StartCoroutine(PanBackCoroutine(winner));
	}
	
	IEnumerator PanBackCoroutine(bool winner){
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
		if (winner){
			win.gameObject.SetActive(true);
		}
		else{
			lose.gameObject.SetActive(true);
		}
		currentScale = targetScale;
	}
}
