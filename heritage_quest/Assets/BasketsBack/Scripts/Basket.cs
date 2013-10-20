using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {
	
	int basketCount;
	
	public tk2dSprite sprite;
	public GameObject countdown;
	
	Vector3 initialScale;
	
	// Use this for initialization
	void Start () {
		basketCount = 3;
		initialScale = countdown.transform.localScale;
	}
	
	// Returns true when basket has been removed
	public bool DecrementBasketCount(){
		basketCount--;
		StartCoroutine(Countdown(basketCount+1));
		if (basketCount == 0){
			basketCount--;
			return false;
		}
		// Avoid duplicate basket removals or something
		else if (basketCount < 0){
			StartCoroutine(Countdown(0));
			return true;
		}
		else {
			return false;
		}
	}
	
	IEnumerator Countdown(int number){
		Debug.Log ("whoops");
		bool counting = true;
		sprite.SetSprite(sprite.GetSpriteIdByName("" + number));
		countdown.SetActive(true);
		countdown.transform.localScale = initialScale;
		Vector3 currentScale = initialScale;
		while (counting){
			currentScale.x = Mathf.Lerp (currentScale.x, -1, Time.deltaTime*1.2f);
			currentScale.y = Mathf.Lerp (currentScale.y, -1, Time.deltaTime*1.2f);
			countdown.transform.localScale = currentScale;
			if (currentScale.x < 1){
				counting = false;
			}
			yield return null;
		}
		countdown.SetActive(false);
		if (number == 0){
			gameObject.SetActive(false);
			Score score = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Score>();
			score.IncrementScore();
		}
	}
}
