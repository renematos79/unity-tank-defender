﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

	public GameObject Enemy;
	public float EnemySpeed = 2.5f;
	public float BulletSpeed = 40f;
	public int Enemies = 50;
	public UnityEngine.UI.Text LabelEnemies;

	private int _Points;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreateEnemyHelicopter", 5.0f, 2.0f);
		_Points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnemies ();
	}

	public void UpdateEnemies(){
		if (this.LabelEnemies != null) {
			this.LabelEnemies.text = this._Points.ToString ();
		}
	}

	public void AddPoints(int points){
		this._Points += points;
	}

	void CreateEnemyHelicopter(){
		var bounds = CameraExtensions.OrthographicBounds (Camera.main);
		float y = Random.value * 4;
		// cria o inimigo
		var enemy = Instantiate (Enemy);
		enemy.transform.position = new Vector2 (bounds.size.x, y);
		// parametro do inimigo
		var moveEnemy = enemy.GetComponent<MoveEnemy> ();
		moveEnemy.Velocity = EnemySpeed;
		moveEnemy.BulletSpeed = BulletSpeed;

		Enemies--;
		if (Enemies <= 0) {
			CancelInvoke ();
		}
	}
}
