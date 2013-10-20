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
		if (transform.parent.gameObject.tag == "Enemy"){
			fallPoints = 1;
		}
		else{
			fallPoints = 5;
		}
		maxPoints = fallPoints;
		spriteWidth = fallbar.scale.x;
	}
	
	void Update () {
		Vector3 scale = fallbar.scale;
		scale.x = Mathf.Lerp(scale.x, target, Time.deltaTime * 5);	
		fallbar.scale = scale;
	}
	
	public void GetHit(bool left){
		if (canHit){
			fallPoints--;
			if (fallPoints <= 0){
				canHit = false;
				if (transform.parent.gameObject.tag == "Enemy"){
					StartCoroutine(SpinAroundAndThenDie(left));
				}
				else{
					player.GetComponent<MovePlayer>().KnockPlayerDown();
					fallPoints = maxPoints;	
				}
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
	
	IEnumerator SpinAroundAndThenDie(bool left){
		float value = GameObject.FindGameObjectWithTag("Player").transform.position.x;
		float randx;
		if (left){
			randx = Random.Range(value - 70, value - 80);
		}
		else{
			randx = Random.Range(value + 70, value + 80);
		}
		
		Vector3 dest = new Vector3(randx, Random.Range (-30,60), transform.parent.position.z);
		
		Debug.Log (dest);
		int count = 100;
		
		int spin = 5;
		if (Random.Range(0,4) > 2){
			spin *= -1;
		}
		
		while (count > 0){
			transform.parent.position = Vector3.Lerp(transform.parent.position, dest, Time.deltaTime * .8f);
			transform.parent.Rotate(new Vector3(0,0,-5));
			count--;
			yield return null;
		}
		Destroy (transform.parent.gameObject);
	}
}
