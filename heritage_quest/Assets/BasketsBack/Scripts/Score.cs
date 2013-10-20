using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	private int score = 0;
	
	public void IncrementScore(){
		score++;
		var sprite = GetComponentInChildren<tk2dSprite>();
		sprite.SetSprite(sprite.GetSpriteIdByName("baskets000" + score));
		
		if (score >= 2){
			Debug.Log ("YOU WIN");
			// TODO: WIN
		}
	}
	
}
