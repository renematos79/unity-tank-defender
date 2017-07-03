using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTankBullet : MonoBehaviour {
	public float Velocity = 2.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (this.Velocity * Time.deltaTime, 0));
	}
}
