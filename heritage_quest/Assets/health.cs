using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	
	bool ishurt = false;
	bool gameOver = false;
	public int hp = 1;
	
	bool tookScreen = false;
	GameObject victoryPanel;
	int count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If player entercollides with wall, health starts to deplete
		if (col.gameObject.tag == "corner" || col.gameObject.tag == "T" || col.gameObject.tag == "end" || col.gameObject.tag == "straight" ){
			ishurt = true;
		}
	}
	void OnCollisionExit(Collision col){	
		//If player exitcollides with wall, change hurtblock back to block
		if (col.gameObject.tag == "corner" || col.gameObject.tag == "T" || col.gameObject.tag == "end" || col.gameObject.tag == "straight"){
			ishurt = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (ishurt){
			if (hp == 0){
				gameOver = true;
				Screencap screen = Camera.main.GetComponent<Screencap>();
				screen.TakeScreenshot(SetTookScreen);
			}
			else
				hp -= 1;
		}
		if (tookScreen && count < 1){
			victoryPanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
			victoryPanel.renderer.material = Camera.main.GetComponent<Screencap>().GetMaterial();
			victoryPanel.transform.localScale = new Vector3(49, 37, 1);
			victoryPanel.transform.eulerAngles = new Vector3(0, 0, 180);
			victoryPanel.transform.position = new Vector3(0, -.04f, -1);
			GameObject.FindGameObjectWithTag("Heritage").GetComponent<Heritage>().SetInnerPanel(victoryPanel, false);
			count = 1;
			tookScreen = false;
		}
	}
	
	public void SetTookScreen(){
		tookScreen = true;
	}
}
