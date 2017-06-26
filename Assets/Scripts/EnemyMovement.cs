using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float Velocity = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var deltaX =  Time.deltaTime * this.Velocity;
		this.transform.Translate (new Vector2 (0, deltaX));
	}
}
