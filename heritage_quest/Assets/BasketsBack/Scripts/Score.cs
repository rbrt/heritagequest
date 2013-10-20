using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	int score = 0,
		count = 0;
	
	bool tookScreen = false;
	
	GameObject victoryPanel;
	
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
	
	public void IncrementScore(){
		score++;
		var sprite = GetComponentInChildren<tk2dSprite>();
		sprite.SetSprite(sprite.GetSpriteIdByName("baskets000" + score));
		
		if (score >= 2){
			Debug.Log ("YOU WIN");
			Screencap screen = Camera.main.GetComponent<Screencap>();
			screen.TakeScreenshot(SetTookScreen);
			
		}
	}
	
	public void SetTookScreen(){
		tookScreen = true;
	}
	
}
