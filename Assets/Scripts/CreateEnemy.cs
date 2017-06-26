using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour {

	public GameObject Enemy;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreateEnemyShip", 5.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateEnemyShip(){
		float y = 10.0f * Random.value - 5;
		var enemy = Instantiate (Enemy);
		enemy.transform.position = new Vector2 (10.0f, y);
	}
}
