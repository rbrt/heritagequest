using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scrollin : MonoBehaviour {
	
	public GameObject player,
					  leftBoundary,
					  rightBoundary,
					  leftBasket,
					  rightBasket,
					  ladder,
					  spawn;
	
	float scrollSpeed = 40;
	
	List<GameObject> enemies;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AddEnemy(GameObject enemy){
		if (enemies == null){
			enemies = new List<GameObject>();
		}
		enemies.Add (enemy);
	}
	
	public void RemoveEnemy(GameObject enemy){
		for (int i = 0; i < enemies.Count; i++){
			if (enemy == enemies[i]){
				enemies.RemoveAt(i);
			}
		}
	}
	
	public void scrollLeft(){
		Vector3 groundPos = transform.position;
		Vector3 leftBasketPos = leftBasket.transform.position;
		Vector3 rightBasketPos = rightBasket.transform.position;
		Vector3 spawnPos = spawn.transform.position;
		
		
		if (!(groundPos.x < -70)){
			
			foreach (var enemy in enemies){
				Vector3 enemyPos = enemy.transform.position;
				enemyPos.x = Mathf.Lerp(enemyPos.x, enemyPos.x - 1, Time.deltaTime * scrollSpeed);
				enemy.transform.position = enemyPos;
			}
			
			spawnPos.x = Mathf.Lerp(spawnPos.x, spawnPos.x - 1, Time.deltaTime * scrollSpeed);
			groundPos.x = Mathf.Lerp(groundPos.x, groundPos.x - 1, Time.deltaTime * scrollSpeed);
			leftBasketPos.x = Mathf.Lerp(leftBasketPos.x, leftBasketPos.x - 1, Time.deltaTime * scrollSpeed);
			rightBasketPos.x = Mathf.Lerp(rightBasketPos.x, rightBasketPos.x - 1, Time.deltaTime * scrollSpeed);
			
			transform.position = groundPos;
			spawn.transform.position = spawnPos;
			leftBasket.transform.position = leftBasketPos;
			rightBasket.transform.position = rightBasketPos;
		}
	}
	
	public void scrollRight(){
		Vector3 groundPos = transform.position;
		Vector3 leftBasketPos = leftBasket.transform.position;
		Vector3 rightBasketPos = rightBasket.transform.position;
		Vector3 spawnPos = spawn.transform.position;
		
		if (!(groundPos.x > 50)){
			
			foreach (var enemy in enemies){
				Vector3 enemyPos = enemy.transform.position;
				enemyPos.x = Mathf.Lerp(enemyPos.x, enemyPos.x + 1, Time.deltaTime * scrollSpeed);
				enemy.transform.position = enemyPos;
			}
			
			spawnPos.x = Mathf.Lerp(spawnPos.x, spawnPos.x + 1, Time.deltaTime * scrollSpeed);
			groundPos.x = Mathf.Lerp(groundPos.x, groundPos.x + 1, Time.deltaTime * scrollSpeed);
			leftBasketPos.x = Mathf.Lerp(leftBasketPos.x, leftBasketPos.x + 1, Time.deltaTime * scrollSpeed);
			rightBasketPos.x = Mathf.Lerp(rightBasketPos.x, rightBasketPos.x + 1, Time.deltaTime * scrollSpeed);
			
			transform.position = groundPos;
			spawn.transform.position = spawnPos;
			leftBasket.transform.position = leftBasketPos;
			rightBasket.transform.position = rightBasketPos;
		}
	}
		
	
}
