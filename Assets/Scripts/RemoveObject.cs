using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour {

	public float RemoveAfter = 0.15f;

	// Use this for initialization
	void Start () {
		Invoke ("RemoveGameObject", this.RemoveAfter);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void RemoveGameObject(){
		DestroyObject (this.gameObject);
	}
}
