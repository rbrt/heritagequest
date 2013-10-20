using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	
	public GameObject rightSpawn,
					  leftSpawn,
					  enemyPrefab,
					  player,
					  fallBar;
	
	public Scrollin scroll;
	
	int enemyCap = 15,
		enemiesOnScreen = 0;
	
	bool initialSpawn = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEnemies());
	}
	
	// Update is called once per frame
	void Update () {
		if (initialSpawn){
			if (enemiesOnScreen < enemyCap){
				SpawnSingle();
			}
		}
	}
	
	
	void SpawnSingle(){
		int value = Random.Range(0,10);
		GameObject enemy;
		if (value > 5){
			enemy = Instantiate(enemyPrefab, rightSpawn.transform.position, rightSpawn.transform.rotation) as GameObject;
		}
		else{
			enemy = Instantiate(enemyPrefab, leftSpawn.transform.position, rightSpawn.transform.rotation) as GameObject;
		}
		enemy.transform.parent = transform.parent;
		enemy.GetComponent<Enemy>().player = player;
		enemy.GetComponentInChildren<FallBar>().player = player;
		enemy.GetComponent<Enemy>().fallBar = fallBar;
		scroll.AddEnemy(enemy);
		enemiesOnScreen++;
	}
	
	IEnumerator SpawnEnemies(){
		while(enemiesOnScreen <= enemyCap){
			SpawnSingle();
			yield return new WaitForSeconds(1);
		}
		initialSpawn = true;
	}
}
