using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private enum Orientation {Left, Right};

	public float Speed = 0.5f;
	public float WeaponRotateAngle = 0.0f;
	public float WeaponRotateSpeed = 0.7f;
	public GameObject Weapon;

	private float WEAPON_ROTATE_ANGULE_MAX = 80f;
	private float WEAPON_ROTATE_ANGULE_MIN = 15f;

	private Animator anim;
	private Orientation State;


	// Use this for initialization
	void Start () {
		this.anim = GetComponent<Animator>();
		this.State = Orientation.Right;
		this.WeaponRotateAngle = WEAPON_ROTATE_ANGULE_MIN;
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

	// Update is called once per frame
	void Update () {
		var idle = true;

		// direita
		if (Input.GetKey (KeyCode.RightArrow)) {
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

	}
}