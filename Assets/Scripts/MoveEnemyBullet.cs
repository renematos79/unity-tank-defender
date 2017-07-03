using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyBullet : MonoBehaviour {

	public float Velocity = 2.5f;
	public AudioClip RicocheteAudioClip = null;
	public AudioClip RicocheteGroundAudioClip = null;
	public float Damage = 2.0f;
	public GameObject ParticleWhenGroundColision;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (-1 * this.Velocity * Time.deltaTime, 0));
	}

	public void OnTriggerEnter2D(Collider2D sender){
		if (sender.gameObject.CompareTag ("Player")) {
			Destroy(this.gameObject);
			if (this.RicocheteAudioClip != null) {
				AudioManager.Instance.PlayClip (this.RicocheteAudioClip);	
			}
			sender.gameObject.SendMessage ("Damage", this.Damage);
		}

		if (sender.gameObject.CompareTag ("Ground")) {
			print("Ground Colision");
			// som de coliscao no chao
			if (this.RicocheteGroundAudioClip != null) {
				AudioManager.Instance.PlayClip (this.RicocheteGroundAudioClip);	
			}
			// cria a particula para simular o contato da bala no chao
			if (this.ParticleWhenGroundColision != null) {
				Instantiate (this.ParticleWhenGroundColision, this.gameObject.transform);
			}
			// destroi a bala
			Destroy(this.gameObject);
		}
	}

}
