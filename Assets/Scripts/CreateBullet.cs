using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateBullet : MonoBehaviour {

	public GameObject Weapon;
	public GameObject Bullet;
	public GameObject Player;
	public GameObject Explosion;
	public int Bullets = 25;
	public UnityEngine.UI.Text LabelBullets;

	public AudioClip BulletAudioClip;
	public float BulletForce = 200f;


	private PlayerMovement _PlayerMovement;

	// Use this for initialization
	void Start () {
		if (this.Player != null) {
			this._PlayerMovement = this.Player.GetComponent<PlayerMovement>();	
		}
	}

	void UpdateBullets(){
		if (this.LabelBullets != null) {
			this.LabelBullets.text = this.Bullets.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (this._PlayerMovement != null) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Bullets--;
				if (Bullets < 0) {
					Bullets = 0;
					EditorApplication.Beep ();
				} else {
					if (this.BulletAudioClip != null) {
						AudioManager.Instance.PlayClip (this.BulletAudioClip);	
					}
					// se ha uma explosao entao cria o objeto e depois cria a bala
					if (this.Explosion != null) {
						CreateExplosion ();
					} 
					ApplyForce ();
				}
			}	
		}		
		UpdateBullets ();
	}

	void CreateExplosion(){
		var exp = Instantiate (Explosion, this.Weapon.gameObject.transform.position, Quaternion.identity);
		var anim = exp.GetComponent<Animator> ();
		anim.SetBool ("animator", true);
	}

	void ApplyForce(){
		var x = this.BulletForce * Mathf.Cos (this._PlayerMovement.WeaponRotateAngle * Mathf.Deg2Rad);
		var y = this.BulletForce * Mathf.Sin (this._PlayerMovement.WeaponRotateAngle * Mathf.Deg2Rad);
		var bulletPosition = new Vector3 (this.Weapon.transform.position.x, this.Weapon.transform.position.y, this.Weapon.transform.position.z);
		var bullet = Instantiate (Bullet, bulletPosition, Quaternion.Euler(bulletPosition));
		bullet.transform.eulerAngles = new Vector3 (0, 0, this._PlayerMovement.WeaponRotateAngle);
		bullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x, y));	
	}
}
