using UnityEngine;
using System.Collections;

public class winner : MonoBehaviour {
	
	GameObject victoryPanel;
	bool tookScreen = false;
	int count = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
		if (tookScreen && count < 1){
			victoryPanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
			victoryPanel.renderer.material = Camera.main.GetComponent<Screencap>().GetMaterial();
			victoryPanel.transform.localScale = new Vector3(49, 37, 1);
			victoryPanel.transform.eulerAngles = new Vector3(0, 0, 180);
			victoryPanel.transform.position = new Vector3(0, -.04f, -1);
			GameObject.FindGameObjectWithTag("Heritage").GetComponent<Heritage>().SetInnerPanel(victoryPanel, true);
			count = 1;
			tookScreen = false;
		}
		
	}
	
	//Collision Detection
	void OnCollisionEnter(Collision col){
		//If trigger entercollides with pan, player has won the game
		if (col.gameObject.tag == "Player"){
			if (col.gameObject.GetComponent<movement>().getLocked()){
				Debug.Log("congrats you won");
				var heritage = GameObject.FindGameObjectWithTag("Heritage").GetComponent<Heritage>();
				
				Screencap screen = Camera.main.GetComponent<Screencap>();
				screen.TakeScreenshot(SetTookScreen);
			}
		}
	}
	
	public void SetTookScreen(){
		tookScreen = true;
	}

}
