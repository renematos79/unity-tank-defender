using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource Sons;
	public static AudioManager Instance = null;

	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayClip(AudioClip clip, bool looping = false)
	{
		Sons.clip = clip;
		Sons.loop = looping;
		Sons.Play ();
	}

	public void Stop(){
		Sons.Stop ();
	}

	public AudioClip GetCurrentAudioClip(){
		return this.Sons.isPlaying ? this.Sons.clip : null;
	}
}
