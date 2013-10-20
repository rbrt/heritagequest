using UnityEngine;
using System.Collections;

public class TImer : MonoBehaviour {
	
	int time = 60,
		count = 0;
	
	public tk2dSprite countdown;
	
	float target,
		  spriteWidth,
		  maxTime;
	
	bool tookScreen,
		 tick;
	public GameObject heritagePanel;
	GameObject victoryPanel;
	
	// Use this for initialization
	void Start () {
		target = countdown.scale.x;
		spriteWidth = target;
		maxTime = time;
		tick = true;
		
		tookScreen = false;
		StartCoroutine(CountdownCoroutine());
		
	}
	
	void Update(){
		Vector3 scale = countdown.scale;
		scale.x = Mathf.Lerp(scale.x, target, Time.deltaTime * 5);	
		countdown.scale = scale;
		
		if (tookScreen && count < 1){
			victoryPanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
			victoryPanel.renderer.material = Camera.main.GetComponent<Screencap>().GetMaterial();
			victoryPanel.transform.localScale = new Vector3(49, 37, 1);
			victoryPanel.transform.eulerAngles = new Vector3(0, 0, 180);
			victoryPanel.transform.position = new Vector3(0, -.04f, -1);
			GameObject.FindGameObjectWithTag("Heritage").GetComponent<Heritage>().SetInnerPanel(victoryPanel);
			count = 1;
			tookScreen = false;
		}
	}
	
	
	
	IEnumerator CountdownCoroutine(){
		Screencap screen = Camera.main.GetComponent<Screencap>();
		while (tick){
			time--;
			target = (spriteWidth / maxTime * time);
			
			if (time < 0 && count == 0){
				screen.TakeScreenshot(SetTookScreen);	
				tick = false;			
			}
			yield return new WaitForSeconds(1);
		}
	}
	
	public void SetTookScreen(){
		tookScreen = true;
	}

	
}
