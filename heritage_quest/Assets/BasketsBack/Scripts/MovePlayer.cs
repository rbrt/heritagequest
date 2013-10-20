using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public GameObject ladderPrefab,
					  ground,
					  leftBasket,
					  rightBasket,
					  hitbox;
	
	public FallBar fallbar;
	
	Scrollin scrollin;
	
	GameObject playerLadder;
	
	bool isMovingLeft = false,
		 isMovingRight = false,
		 isJumping = false,
		 ladderInPlay = false,
		 isClimbingUp = false,
		 isClimbingDown = false,
		 isPunching = false,
		 isFacingLeft = true, // punch direction
		 isInLeftBasket = false,
		 isInRightBasket = false,
		 isKnockedDown = false;
	
	float jumpVal = 10,
		  moveSpeed = 30,
	 	  jumpSpeed = 10,
		  climbingSpeed = 10,
		  startHeight;
	
	void Start(){
		scrollin = ground.GetComponent<Scrollin>();
		startHeight = transform.position.y;
	}
	
	void Update () {
		if (!isKnockedDown){
			// Left
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
				isMovingLeft = true;
				isFacingLeft = true;
				isMovingRight = false;
				StartCoroutine(RunLeftCoroutine());
			}
			// Right
			else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
				isMovingRight = true;
				isMovingLeft = false;
				isFacingLeft = false;
				StartCoroutine(RunRightCoroutine());
			}
			// Up - (climbing i guess?)
			else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
				if (ladderInPlay){
					isClimbingUp = true;
					isClimbingDown = false;
				}	
			}
			// Down (climbing i guess?)
			else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
				if (ladderInPlay){
					isClimbingDown = true;
					isClimbingUp = false;
				}	
			}
			// Jump
			else if (Input.GetKeyDown(KeyCode.Space) && !isJumping){
				isJumping = true;
				StartCoroutine(JumpCoroutine());
			}
			else if (Input.GetKeyDown(KeyCode.LeftControl)){
				if (!ladderInPlay){
					playerLadder = Instantiate(ladderPrefab, transform.position, transform.rotation) as GameObject;	
					scrollin.ladder = playerLadder;
					ladderInPlay = true;
				}
				else{
					Destroy(playerLadder);
					ladderInPlay = false;
					if (transform.position.y > startHeight){
						StartCoroutine(FallCoroutine());
					}
				}
			}
			else if (Input.GetKeyDown(KeyCode.LeftShift)){
				if (!isPunching){
					isPunching = true;
					if (isFacingLeft){
						StartCoroutine(PunchLeftCoroutine());
					}
					else{
						StartCoroutine(PunchRightCoroutine());
					}
				}
			}
			else if (Input.GetKeyDown(KeyCode.B)){
				GetComponent<AudioSource>().Play();
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
				if (ladderInPlay){
					isClimbingUp = false;
				}	
			}
			// Down (climbing i guess?)
			else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
				if (ladderInPlay){
					isClimbingDown = false;
				}	
			}
			
			Vector3 pos = transform.position;
			if (isMovingLeft && !ladderInPlay ){
				if (pos.x <= - 10){
					scrollin.scrollRight();
				}
				else{
					pos.x = Mathf.Lerp(transform.position.x, transform.position.x - 1, Time.deltaTime * moveSpeed);
				}
			}
			else if (isMovingRight && !ladderInPlay){
				if (pos.x >= 10){
					scrollin.scrollLeft();
				}
				else{
					pos.x = Mathf.Lerp(transform.position.x, transform.position.x + 1, Time.deltaTime * moveSpeed);
				}
			}
			if (isClimbingUp){
				if (pos.y < startHeight + 15){
					pos.y = Mathf.Lerp(pos.y, pos.y + 1, Time.deltaTime * climbingSpeed);	
				}
			}
			else if (isClimbingDown){
				if (pos.y > startHeight){
					pos.y = Mathf.Lerp(pos.y, pos.y - 1, Time.deltaTime * climbingSpeed);
				}
			}
			
			transform.position = pos;
		}
	}
	
	public void KnockPlayerDown(){
		isKnockedDown = true;
		isMovingLeft = false;
		isMovingRight = false;
		isPunching = false;
		isInLeftBasket = false;
		isInRightBasket = false;
		StartCoroutine(KnockDownCoroutine());
	}
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "LeftBasket"){
			isInLeftBasket = true;
			StartCoroutine(RemoveLeftBasketCoroutine());
		}
		else if (collision.gameObject.name == "RightBasket"){
			isInRightBasket = true;
			StartCoroutine(RemoveRightBasketCoroutine());
		}
	}
	
	void OnCollisionExit(Collision collision){
		if (collision.gameObject.name == "LeftBasket"){
			isInLeftBasket = false;
		}
		else if (collision.gameObject.name == "RightBasket"){
			isInRightBasket = false;
		}
	}
	
	IEnumerator JumpCoroutine(){
		bool isFalling = false;
		bool jumpin = true;
		float jumpHeight = transform.position.y + jumpVal;
		float baseHeight = transform.position.y;
		while (jumpin){
			if (transform.position.y >= jumpHeight -2){
				isFalling = true;
				jumpin = false;
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
				isJumping = false;
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
	
	IEnumerator FallCoroutine(){
		bool isFalling = true;
		while (isFalling){
			if (transform.position.y <= startHeight + 2){
				isFalling = false;
				Vector3 pos = transform.position;
				pos.y = startHeight;
				transform.position = pos;
			}
			else{
				float height = Mathf.Lerp(transform.position.y, startHeight, Time.deltaTime * jumpSpeed);
				Vector3 pos = transform.position;
				pos.y = height;
				transform.position = pos;
				yield return null;
			}
		}
	}
	
	IEnumerator PunchLeftCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("punchleft1"),
								  sprite.GetSpriteIdByName("punchleft2"),
								  sprite.GetSpriteIdByName("punchleft1"),
								  sprite.GetSpriteIdByName("rudeguy")};
		while (isPunching){
			if (count >= indices.Length){
				isPunching = false;
			}
			else{
				if (count == 1){
					RaycastHit hit;
					Vector3 offset = transform.position;
					offset.x += 2;
					if (Physics.Raycast(offset, -1 * Vector3.right, out hit, 6f)){
						hit.collider.gameObject.GetComponent<Enemy>().GetHit(true);	
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
		int[] indices = new int[]{sprite.GetSpriteIdByName("punchright1"),
								  sprite.GetSpriteIdByName("punchright2"),
								  sprite.GetSpriteIdByName("punchright1"),
								  sprite.GetSpriteIdByName("rudeguy")};
		while (isPunching){
			if (count >= indices.Length){
				isPunching = false;
			}
			else{
				if (count == 1){
					RaycastHit hit;
					Vector3 offset = transform.position;
					offset.x -= 2;
					if (Physics.Raycast(offset, Vector3.right, out hit, 6f)){
						hit.collider.gameObject.GetComponent<Enemy>().GetHit(false);	
						Debug.Log (hit.collider.gameObject.name);
					}
				}
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.1f);
			}
		}
	}
	
	IEnumerator RemoveLeftBasketCoroutine(){
		Basket basket = leftBasket.GetComponent<Basket>();
		bool removin = true;
		while (removin){
			bool basketDone = basket.DecrementBasketCount();
			if (basketDone){
				removin = false;
				basket.gameObject.SetActive(false);
				Debug.Log ("YOU GOT THAT BASKET BACK");
			}
			yield return new WaitForSeconds(1);
		}
	}
	
	IEnumerator RemoveRightBasketCoroutine(){
		Basket basket = rightBasket.GetComponent<Basket>();
		bool removin = true;
		while (removin){
			bool basketDone = basket.DecrementBasketCount();
			if (basketDone){
				removin = false;
				basket.gameObject.SetActive(false);
				Debug.Log ("YOU GOT THAT BASKET BACK");
			}
			yield return new WaitForSeconds(1);
		}
	}
	
	IEnumerator RunLeftCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("runleft1"),
								  sprite.GetSpriteIdByName("runleft2"),
								  sprite.GetSpriteIdByName("runleft1"),
								  sprite.GetSpriteIdByName("rudeguy")};
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
		int[] indices = new int[]{sprite.GetSpriteIdByName("runright1"),
								  sprite.GetSpriteIdByName("runright2"),
								  sprite.GetSpriteIdByName("runright1"),
								  sprite.GetSpriteIdByName("rudeguy")};
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
	
	IEnumerator KnockDownCoroutine(){
		tk2dSprite sprite = GetComponent<tk2dSprite>();
		bool gettingHit = true;
		
		int count = 0;
		int[] indices = new int[]{sprite.GetSpriteIdByName("playerfall1"),
								  sprite.GetSpriteIdByName("playerfall2"),
								  sprite.GetSpriteIdByName("playerfall1"),
								  sprite.GetSpriteIdByName("playerfall2"),
								  sprite.GetSpriteIdByName("playerfall1"),
								  sprite.GetSpriteIdByName("playerfall2"),
								  sprite.GetSpriteIdByName("playerfall1"),
								  sprite.GetSpriteIdByName("playerfall2"),};
		while (gettingHit){
			if (count >= indices.Length){
				gettingHit = false;
				isKnockedDown = false;
				sprite.SetSprite(sprite.GetSpriteIdByName("rudeguy"));
			}
			else{
				sprite.SetSprite(indices[count]);
				count++;	
				yield return new WaitForSeconds(.4f);
			}
		}
		fallbar.CanHitAgain();
	}
}
