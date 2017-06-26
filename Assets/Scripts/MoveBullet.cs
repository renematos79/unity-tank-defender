using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

	public float Velocity = 2.5f;
	public AudioClip ExplosionAudioClip;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (this.Velocity * Time.deltaTime, 0));
	}

	public void OnCollisionEnter2D(Collision2D sender){

		if (sender.gameObject.CompareTag ("Enemy")) {
			print("Colisao");
			Destroy(sender.gameObject);
			Destroy(this.gameObject);
			if (this.ExplosionAudioClip != null) {
				AudioManager.Instance.PlayClip (this.ExplosionAudioClip);	
			}
		}


	}
}
