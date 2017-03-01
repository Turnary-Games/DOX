using UnityEngine;
using System.Collections;

public class Twitch : MonoBehaviour {

	public float min;
	public float max;

	// Use this for initialization
	void Start  () {
		Invoke ("twitch", Random.Range (min, max));
	}

	void twitch (){
		GetComponentInChildren<Animator> ().SetTrigger ("twitch");
		Invoke ("twitch", Random.Range (min, max));
	}
}
