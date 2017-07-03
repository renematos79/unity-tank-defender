using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private enum Orientation {Left, Right};

	public float Speed = 0.5f;
	public float WeaponRotateAngle = 0.0f;
	public float WeaponRotateSpeed = 0.7f;
	public GameObject Weapon;
	public AudioClip TankMovementAudioClip = null;
	public AudioClip TankExplosionAudioClip = null;
	public GameObject Smoke = null;
	public float Armor = 100.0f;

	public UnityEngine.UI.Text LabelArmor;


	private float WEAPON_ROTATE_ANGULE_MAX = 80f;
	private float WEAPON_ROTATE_ANGULE_MIN = 15f;
	private float START_SMOKE_WHEN = 40f;

	private Animator anim;
	private Orientation State;
	private SpriteRenderer _SpriteRender;

	// Use this for initialization
	void Start () {
		this.anim = GetComponent<Animator>();
		this.State = Orientation.Right;
		this.WeaponRotateAngle = WEAPON_ROTATE_ANGULE_MIN;
		this._SpriteRender = GetComponent<SpriteRenderer> ();
	}

	private void TurnRight(){
		anim.SetBool ("idle", true);
	}

	private void WalkRight(){
		this.gameObject.transform.Translate (new Vector2 (this.Speed * Time.deltaTime, 0));
		anim.SetBool ("idle", false);
	}

	private void TurnLeft(){
		anim.SetBool ("idle", true);
	}

	private void WalkLeft(){
		this.gameObject.transform.Translate (new Vector2 (-1 * this.Speed * Time.deltaTime, 0));
		anim.SetBool ("idle", false);
	}

	private void DestroyTank(){
		anim.SetBool ("destroy", true);
		Weapon.SetActive (false);
		AudioManager.Instance.PlayClipIfNeed (TankExplosionAudioClip);
	}

	private void PlayTankMovementAudioClip(){
		if (TankMovementAudioClip != null) {
			if (AudioManager.Instance.GetCurrentAudioClip () != TankMovementAudioClip) {
				AudioManager.Instance.PlayClip (TankMovementAudioClip);	
			}
		}
	}

	private void Damage(float value){
		this.Armor -= value;
		UpdateArmor();
	}

	private void IncreaseArmor(float value){
		this.Armor += value;
		if (this.Armor >= 100f) {
			this.Armor = 100f;
		}
	}

	private void UpdateArmor(){
		if (this.Armor > 0 && this.Armor < START_SMOKE_WHEN) {
			this.Smoke.SetActive (true);
			if (_SpriteRender.color == Color.white) {
				_SpriteRender.color = Color.red;
			} else {
				_SpriteRender.color = Color.white;
			}	
		} else if (this.Armor <= 0) {
			DestroyTank ();
		} else {
			_SpriteRender.color = Color.white;
			this.Smoke.SetActive (false);
		}
		// update armor label
		if (this.LabelArmor != null){
			this.LabelArmor.text = this.Armor.ToString ("0.00");
		}
	}

	public bool IsAlive(){
		return (anim.GetBool ("destroy") == false);
	}

	// Update is called once per frame
	void Update () {
		if (this.IsAlive() == false) {
			return;
		}

		var idle = true;

		//if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) {
		//	PlayTankMovementAudioClip ();
		//} 

		// direita
		if (Input.GetKey (KeyCode.RightArrow)) {
			PlayTankMovementAudioClip ();
			if (this.State == Orientation.Left) { 
				this.TurnRight ();
			} else {
				this.WalkRight ();
			}
			this.State = Orientation.Right;
			idle = false;
		} 

		// esquerda
		if (Input.GetKey (KeyCode.LeftArrow)) {
			PlayTankMovementAudioClip ();
			if (this.State == Orientation.Right) { 
				this.TurnLeft ();	
			} else {
				this.WalkLeft ();
			}
			this.State = Orientation.Left;
			idle = false;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			WeaponRotateAngle += WeaponRotateSpeed;
			if (WeaponRotateAngle > WEAPON_ROTATE_ANGULE_MAX)
				WeaponRotateAngle = WEAPON_ROTATE_ANGULE_MAX;
			this.Weapon.transform.eulerAngles = new Vector3 (0, 0, WeaponRotateAngle);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			WeaponRotateAngle -= WeaponRotateSpeed;
			if (WeaponRotateAngle < WEAPON_ROTATE_ANGULE_MIN)
				WeaponRotateAngle = WEAPON_ROTATE_ANGULE_MIN;
			
			this.Weapon.transform.eulerAngles = new Vector3 (0, 0, WeaponRotateAngle);
		}


		if (idle) {
			if (this.State == Orientation.Left) { 
				this.TurnLeft ();
			} else {
				this.TurnRight ();
			}
		}

		UpdateArmor ();
		IncreaseArmor (0.01f);
	}
}