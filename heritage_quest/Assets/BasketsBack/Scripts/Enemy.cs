using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject player,
				      fallBar,
					  selfFallBar;
	
	bool isMovingLeft = false,
		 isMovingRight = false,
		 isPunching = false;
	
	float moveSpeed = 10;
	
	
	// Update is called once per frame
	void Update () {
		
		if (!isPunching){
			// Player to the left
			if (player.transform.position.x < transform.position.x - 4){
				if (!isMovingLeft){
					isMovingLeft = true;
					isMovingRight = false;
					StartCoroutine(RunLeftCoroutine());
				}
			}
			// Player to the right
			else if (player.transform.position.x > transform.position.x + 4){
				if (!isMovingRight){
					isMovingRight = true;
					isMovingLeft = false;
					StartCoroutine(RunRightCoroutine());
				}
			}
			// Player is close to the enemy
			else{
				isMovingLeft = false;
				isMovingRight = false;
				if (fallBar.GetComponent<FallBar>().GetCanHit()){
					isPunching = true;
					if (player.transform.position.x < transform.position.x){
						StartCoroutine(PunchLeftCoroutine());
					}
					else{
						StartCoroutine(PunchRightCoroutine());
					}
				}
			}
		}
		
		Vector3 pos = transform.position;
		
		if (isMovingRight){
			pos.x  = Mathf.Lerp(pos.x, pos.x + 1, Time.deltaTime * moveSpeed);
		}
		else if (isMovingLeft){
			pos.x  = Mathf.Lerp(pos.x, pos.x - 1, Time.deltaTime * moveSpeed);
		}
		
		transform.position = pos;
	}
	
	
	bool PlayerInRange(){
		return Mathf.Abs(player.transform.position.x - transform.position.x) < 5 &&
			   Mathf.Abs(player.transform.position.y - transform.position.y) < 5;
	}
	
	public void GetHit(bool left){
		selfFallBar.GetComponent<FallBar>().GetHit(left);	
	}
	
	
	IEnumerator RunLeftCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("basketballrunleft1"),
								  sprite.GetSpriteIdByName("basketballrunleft2"),
								  sprite.GetSpriteIdByName("basketballrunleft1"),
								  sprite.GetSpriteIdByName("basketballplayer 1")};
		while (isMovingLeft){
			if (count >= indices.Length){
				count = 0;
			}
			else{
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.1f);
			}
		}
		sprite.SetSprite(indices[indices.Length-1]);
	}
	
	IEnumerator RunRightCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("basketballrunright1"),
								  sprite.GetSpriteIdByName("basketballrunright2"),
								  sprite.GetSpriteIdByName("basketballrunright1"),
								  sprite.GetSpriteIdByName("basketballplayer 1")};
		while (isMovingRight){
			if (count >= indices.Length){
				count = 0;
			}
			else{
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.1f);
			}
		}
		sprite.SetSprite(indices[indices.Length-1]);
	}
	
	IEnumerator PunchLeftCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("basketpunchleft1"),
								  sprite.GetSpriteIdByName("basketpunchleft2"),
								  sprite.GetSpriteIdByName("basketpunchleft1"),
								  sprite.GetSpriteIdByName("basketballplayer 1")};
		while (isPunching){
			if (count >= indices.Length){
				isPunching = false;
			}
			else{
				if (count == 1){
					if (PlayerInRange()){
					fallBar.GetComponent<FallBar>().GetHit(true);
					}
				}
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.1f);
			}
		}
	}
	
	IEnumerator PunchRightCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("basketpunchright1"),
								  sprite.GetSpriteIdByName("basketpunchright2"),
								  sprite.GetSpriteIdByName("basketpunchright1"),
								  sprite.GetSpriteIdByName("basketballplayer 1")};
		while (isPunching){
			if (count >= indices.Length){
				isPunching = false;
			}
			else{
				if (count == 1){
					if (PlayerInRange()){
						fallBar.GetComponent<FallBar>().GetHit(false);
					}
				}
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.1f);
			}
		}
	}
}
