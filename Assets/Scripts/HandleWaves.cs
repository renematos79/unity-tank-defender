using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWaves : MonoBehaviour {

	public Renderer Quad;
	public float OffsetSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		this.Quad = GetComponent<Renderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		var offset = new Vector2(Time.deltaTime * this.OffsetSpeed, 0);
		this.Quad.material.mainTextureOffset += offset;
	}
}
