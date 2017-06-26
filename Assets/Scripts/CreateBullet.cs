using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour {

	public GameObject Weapon;
	public GameObject Bullet;
	public AudioClip BulletAudioClip;
	public float BulletForce = 200f;

	private PlayerMovement Player;

	// Use this for initialization
	void Start () {
		this.Player = this.GetComponent<PlayerMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			ApplyForce ();
			if (this.BulletAudioClip != null) {
				AudioManager.Instance.PlayClip (this.BulletAudioClip);	
			}
		}		
	}

	void ApplyForce(){
		var x = this.BulletForce * Mathf.Cos (this.Player.WeaponRotateAngle * Mathf.Deg2Rad);
		var y = this.BulletForce * Mathf.Sin (this.Player.WeaponRotateAngle * Mathf.Deg2Rad);
		var bulletPosition = new Vector3 (this.Weapon.transform.position.x, this.Weapon.transform.position.y, this.Weapon.transform.position.z);
		var bullet = Instantiate (Bullet, bulletPosition, Quaternion.Euler(bulletPosition));
		bullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x, y));

	}
}
