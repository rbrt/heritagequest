using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

	public	AudioClip burntToast,
					  coldWater,
		 			  pickup;
	
	public AudioSource audio;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayBurntToast(){
		audio.clip = burntToast;
		PlaySound();
	}
	
	public void PlayColdWater(){
		audio.clip = coldWater;
		PlaySound();
	}
	
	public void PlayPickup(){
		audio.clip = pickup;
		PlaySound();
	}
	
	void PlaySound(){
		audio.Play();
	}
}
