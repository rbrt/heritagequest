using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public GameObject ladderPrefab;
	
	bool isMovingLeft = false,
		 isMovingRight = false,
		 isJumping = false,
		 ladderInPlay = false;
	
	float jumpVal = 10,
		  moveSpeed = 20,
	 	  jumpSpeed = 10;
	
	void Update () {
		// Left
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			isMovingLeft = true;
			isMovingRight = false;
		}
		// Right
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			isMovingRight = true;
			isMovingLeft = false;
		}
		// Up - (climbing i guess?)
		else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			
		}
		// Down (climbing i guess?)
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			
		}
		// Jump
		else if (Input.GetKeyDown(KeyCode.Space) && !isJumping){
			isJumping = true;
			StartCoroutine(JumpCoroutine());
		}
		else if (Input.GetKeyDown(KeyCode.LeftShift)){
			var playerLadder = Instantiate(ladderPrefab, transform.position, transform.rotation) as GameObject;
		}
		
		// Left
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
			isMovingLeft = false;		
		}
		// Right
		else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
			isMovingRight = false;
		}
		// Up - (climbing i guess?)
		else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
			
		}
		// Down (climbing i guess?)
		else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
			
		}
		
		Vector3 pos = transform.position;
		if (isMovingLeft){
			pos.x = Mathf.Lerp(transform.position.x, transform.position.x - 1, Time.deltaTime * moveSpeed);
		}
		else if (isMovingRight){
			pos.x = Mathf.Lerp(transform.position.x, transform.position.x + 1, Time.deltaTime * moveSpeed);
		}
		transform.position = pos;
		
	}
	
	IEnumerator JumpCoroutine(){
		bool isFalling = false;
		float jumpHeight = transform.position.y + jumpVal;
		float baseHeight = transform.position.y;
		while (isJumping){
			if (transform.position.y >= jumpHeight -2){
				isFalling = true;
				isJumping = false;
			}
			else{
				float height = Mathf.Lerp(transform.position.y, jumpHeight, Time.deltaTime * jumpSpeed);
				Vector3 pos = transform.position;
				pos.y = height;
				transform.position = pos;
				yield return null;
			}
		}
		while (isFalling){
			if (transform.position.y <= baseHeight + 2){
				isFalling = false;
				Vector3 pos = transform.position;
				pos.y = baseHeight;
				transform.position = pos;
			}
			else{
				float height = Mathf.Lerp(transform.position.y, baseHeight, Time.deltaTime * jumpSpeed);
				Vector3 pos = transform.position;
				pos.y = height;
				transform.position = pos;
				yield return null;
			}
		}
	}
}
