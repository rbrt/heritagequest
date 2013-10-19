using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject player;
	
	bool isMovingLeft = false,
		 isMovingRight = false,
		 isJumping = false,
		 ladderInPlay = false,
		 isClimbingUp = false,
		 isClimbingDown = false,
		 isPunching = false,
		 isFacingLeft = true, // punch direction
		 isInLeftBasket = false,
		 isInRightBasket = false;
	
	float jumpVal = 10,
		  moveSpeed = 1,
	 	  jumpSpeed = 10,
		  climbingSpeed = 10,
		  startHeight;
	
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		// Player to the right
		if (player.transform.position.x < transform.position.x - 4){
			isMovingLeft = true;
			isMovingRight = false;
		}
		// Player to the left
		else if (player.transform.position.x > transform.position.x + 4){
			isMovingRight = true;
			isMovingLeft = false;
		}
		// Player is close to the enemy
		else{
			isMovingLeft = false;
			isMovingRight = false;
		}
		
		Vector3 pos = transform.position;
		
		if (isMovingLeft){
			pos.x  = Mathf.Lerp(pos.x, player.transform.position.x + 1, Time.deltaTime * moveSpeed);
		}
		else if (isMovingRight){
			pos.x  = Mathf.Lerp(pos.x, player.transform.position.x - 1, Time.deltaTime * moveSpeed);
		}
		
		transform.position = pos;
	}
}
