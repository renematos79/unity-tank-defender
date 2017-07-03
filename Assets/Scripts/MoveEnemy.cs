using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	public float Velocity = 2.5f;
	public AudioClip ExplosionAudioClip;
	public AudioClip FireAudioClip;
	public GameObject Weapon;
	public GameObject Bullet;
	public float BulletAngle = 45f;
	public float BulletSpeed = 40f;
	public float WaitingForSecondsBeforeFiring = 3.0f;
	public float ShotInterval = 2.0f;

	private Animator _Animator;
	private bool _AllowFire = false;

	// Use this for initialization
	void Start () {
		_Animator = this.GetComponent<Animator> ();
		Invoke ("StartFire", this.WaitingForSecondsBeforeFiring);
		_AllowFire = false;
	}

	void StartFire(){
		_AllowFire = true;
		if (this.Weapon != null) {
			InvokeRepeating("Fire", 0, ShotInterval);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (-1 * this.Velocity * Time.deltaTime, 0));
		// ativa a animacao para metralhadora on
		if (_AllowFire){
			this.Weapon.SetActive (!this.Weapon.gameObject.activeSelf);	
		}
	}

	void DestroyHelicopter(){
		Destroy(this.gameObject);
	}

	public void OnTriggerEnter2D(Collider2D sender){
		if (sender.gameObject.CompareTag ("Bullet")) {
			Camera.main.SendMessage("AddPoints", 1);

			// desliga o audio anterior
			AudioManager.Instance.Stop ();
			// toca o audio associado a explosao
			if (this.ExplosionAudioClip != null) {
				AudioManager.Instance.PlayClip (this.ExplosionAudioClip);	
			}
			// diminui o tamanho da imagem associada a explosao
			var scale = this.gameObject.transform.localScale;
			scale.x = 0.3f;
			scale.y = 0.3f;
			this.gameObject.transform.localScale = scale;
			// cria a animacao de explosao
			if (this._Animator != null){
				this._Animator.SetBool ("explosion", true);	
			}
			// remove a municao que sofreu colisao
			Destroy(sender.gameObject);
			// remove o inimigo
			Invoke ("DestroyHelicopter", 0.5f);
		}
	}

	void Fire(){
		if (this.FireAudioClip != null) {
			if (AudioManager.Instance.GetCurrentAudioClip () != this.FireAudioClip) {
				AudioManager.Instance.PlayClip (this.FireAudioClip);	
			}
		} 
		CreateBullet ();
	}

	void CreateBullet(){
		var bulletPosition = new Vector3 (this.Weapon.transform.position.x, this.Weapon.transform.position.y, this.Weapon.transform.position.z);
		var bullet = Instantiate (Bullet, bulletPosition, Quaternion.Euler(bulletPosition));
		var moveEnemyBullet = bullet.GetComponent<MoveEnemyBullet> ();
		moveEnemyBullet.Velocity = this.BulletSpeed;

		bullet.transform.eulerAngles = new Vector3 (0, 0, this.BulletAngle);
	}
}
