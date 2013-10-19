using UnityEngine;
using System.Collections;

public class FallBar : MonoBehaviour {
	
	public tk2dSprite fallbar;
	public GameObject player;
	
	float fallPoints,
		  spriteWidth,
		  maxPoints,
		  target;
	
	bool canHit = true;
	
	void Start(){
		target = fallbar.scale.x;
		fallPoints = 5;
		maxPoints = fallPoints;
		spriteWidth = fallbar.scale.x;
	}
	
	void Update () {
		Vector3 scale = fallbar.scale;
		scale.x = Mathf.Lerp(scale.x, target, Time.deltaTime * 5);	
		fallbar.scale = scale;
	}
	
	public void GetHit(){
		if (canHit){
			fallPoints--;
			if (fallPoints <= 0){
				canHit = false;
				player.GetComponent<MovePlayer>().KnockPlayerDown();
				fallPoints = maxPoints;
			}
			target = (spriteWidth / maxPoints * fallPoints);
		}
	}
	
	public void CanHitAgain(){
		canHit = true;
	}
	
	public bool GetCanHit(){
		return canHit;
	}
}
