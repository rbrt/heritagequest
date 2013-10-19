using UnityEngine;
using System.Collections;

public class Scrollin : MonoBehaviour {
	
	public GameObject player,
					  leftBoundary,
					  rightBoundary,
					  leftBasket,
					  rightBasket,
					  ladder;
	
	float scrollSpeed = 40;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void scrollLeft(){
		Vector3 groundPos = transform.position;
		Vector3 leftBasketPos = leftBasket.transform.position;
		Vector3 rightBasketPos = rightBasket.transform.position;
		
		if (!(groundPos.x < -60)){
			groundPos.x = Mathf.Lerp(groundPos.x, groundPos.x - 1, Time.deltaTime * scrollSpeed);
			leftBasketPos.x = Mathf.Lerp(leftBasketPos.x, leftBasketPos.x - 1, Time.deltaTime * scrollSpeed);
			rightBasketPos.x = Mathf.Lerp(rightBasketPos.x, rightBasketPos.x - 1, Time.deltaTime * scrollSpeed);
			
			transform.position = groundPos;
			leftBasket.transform.position = leftBasketPos;
			rightBasket.transform.position = rightBasketPos;
		}
	}
	
	public void scrollRight(){
		Vector3 groundPos = transform.position;
		Vector3 leftBasketPos = leftBasket.transform.position;
		Vector3 rightBasketPos = rightBasket.transform.position;
		if (!(groundPos.x > 40)){
			groundPos.x = Mathf.Lerp(groundPos.x, groundPos.x + 1, Time.deltaTime * scrollSpeed);
			leftBasketPos.x = Mathf.Lerp(leftBasketPos.x, leftBasketPos.x + 1, Time.deltaTime * scrollSpeed);
			rightBasketPos.x = Mathf.Lerp(rightBasketPos.x, rightBasketPos.x + 1, Time.deltaTime * scrollSpeed);
			
			transform.position = groundPos;
			leftBasket.transform.position = leftBasketPos;
			rightBasket.transform.position = rightBasketPos;
		}
	}
		
	
}
