using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEngine : MonoBehaviour {

	public float Velocity = 2.5f;
	public AudioClip ExplosionAudioClip;
	public AudioClip FireAudioClip;
	public GameObject Weapon;
	public GameObject Bullet;
	public float BulletAngle = 45f;
	public float BulletSpeed = 40f;
	public float BulletInterval = 2.0f;
	public float WaitingForSecondsBeforeFiring = 3.0f;
	public GameObject Player;


	private Animator _Animator;
	private bool _AllowFire = false;

	// Use this for initialization
	void Start () {
		_AllowFire = false;
		_Animator = this.GetComponent<Animator> ();
		Invoke ("PrepareToFire", this.WaitingForSecondsBeforeFiring);
	}

	void PrepareToFire(){
		_AllowFire = true;
		InvokeRepeating("Fire", 0, BulletInterval);
	}

	void Fire(){
		if (_AllowFire) {
			AudioManager.Instance.PlayClipIfNeed(this.FireAudioClip);	
			CreateBullet ();	
		}
	}

	void CreateBullet(){
		var bulletPosition = new Vector3 (this.Weapon.transform.position.x, this.Weapon.transform.position.y, this.Weapon.transform.position.z);
		var bullet = Instantiate (Bullet, bulletPosition, Quaternion.Euler(bulletPosition));
		var moveEnemyBullet = bullet.GetComponent<MoveEnemyBullet> ();
		moveEnemyBullet.Velocity = this.BulletSpeed;

		bullet.transform.eulerAngles = new Vector3 (0, 0, this.BulletAngle);
	}

	void MachineGundEffect(){
		if (_AllowFire) {
			this.Weapon.SetActive (!this.Weapon.gameObject.activeSelf);	
		} else {
			this.Weapon.SetActive (false);	
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (-1 * this.Velocity * Time.deltaTime, 0));
		// ativa a animacao e comeca a piscar a metralhadora
		MachineGundEffect();
		// se a posicao do helicoptero for menor que o player desliga a metralhoradora
		if (this.Player != null){
			var xPlayer = this.Player.gameObject.transform.position.x;
			var xHelicopter = this.gameObject.transform.position.x;
			_AllowFire = (xPlayer < xHelicopter);
		}
	}

	void DestroyHelicopter(){
		Destroy(this.gameObject);
	}

	public void OnTriggerEnter2D(Collider2D sender){
		// coliscao com a bala disparada pelo Tanque
		if (sender.gameObject.CompareTag ("Bullet")) {
			// avisa ao GameEngine que houve uma colisao entre o Helicoptero e a Bala disparada pelo Tanque
			Camera.main.SendMessage("AddPoints", 1);
			// toca o audio associado a explosao
			AudioManager.Instance.PlayClipIfNeed(this.ExplosionAudioClip);
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

}
