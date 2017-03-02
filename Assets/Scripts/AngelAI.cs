using UnityEngine;
using System.Collections;

public class AngelAI : MonoBehaviour {

	public Rigidbody bullet;
	public float bulletspeed;
	public Rigidbody wave;
	public float wavespeed;
	public Rigidbody pulse;
	public Transform target;
	public AudioSource source;
	public ScoreTimer scoreTimer;
	
	private bool active = false;

	void Update () {
		if (GameObject.FindGameObjectsWithTag ("pickup").Length == 0)
			Activate();

		if (!active)
			return;

		transform.LookAt (target);
	}

	IEnumerator Attack() {
		while (true){
			yield return new WaitForSeconds(2);
			for (int i=0; i <3;i++){
				yield return new WaitForSeconds(1);
				yield return StartCoroutine("Burst");
			}

			for (int i=0; i <3;i++){
				yield return new WaitForSeconds(1);
				yield return StartCoroutine("Wave");
			}
			
			yield return new WaitForSeconds(3);
			yield return StartCoroutine(Pulse ());
			yield return new WaitForSeconds(3);


			yield return new WaitForSeconds(1);
			yield return StartCoroutine("SuperBurst");

			
		}
	}

	IEnumerator Burst() {

		for (int i = 0; i <3; i++) {
			Rigidbody clone = Instantiate (bullet, transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection (Vector3.forward * bulletspeed);
			yield return new WaitForSeconds(0.5f);

		}

	}

	IEnumerator SuperBurst() {
		
		for (int i = 0; i <30; i++) {
			Rigidbody clone = Instantiate (bullet, transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection (Vector3.forward * bulletspeed);
			yield return new WaitForSeconds(0.2f);
			
		}
		
	}

	IEnumerator Wave() {
		Quaternion rotation = transform.rotation;
		Vector3 angle = rotation.eulerAngles;
		angle.y = angle.y - 180;
		rotation.eulerAngles = angle;

		Rigidbody clone = Instantiate (wave, transform.position, rotation) as Rigidbody;
		clone.velocity = transform.TransformDirection (Vector3.forward * wavespeed);

		yield return new WaitForSeconds(0);
	}

	IEnumerator Pulse() {

		Instantiate (pulse, transform.position, Quaternion.identity);
		
		yield return new WaitForSeconds(1);
	}
	
	public void Activate() {
		if (!active) {
			source.Play();
			active = true;
			StartCoroutine("Attack");
			if (scoreTimer != null) scoreTimer.StartTimer();

			PratilceController.ActivateAll();
		}
	}

	public void Deactivate() {
		if (active) {
			active = false;
			Destroy(this); // One time solution. Can't activate it once deactivated sadly
			if (scoreTimer != null)
				scoreTimer.StopTimer();
		}
	}
}
