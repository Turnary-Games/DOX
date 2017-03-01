using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngelController : MonoBehaviour {

	public AngelAI angelAI;
	public int health = 1;
	public float inveainebleTime;
	[Space(12)]
	public Slider healthbar;
	public ChangeScene fading;
	[Space(12)]
	public float slowmoScale;
	public float slowmoSpeed;

	private bool invinianbel;
	[HideInInspector] public bool dead;

	void Start() {
		HealthUpdate();
	}

	void Update() {
		if (dead) {
			var state = fading.anim.IsInTransition(0)? fading.anim.GetNextAnimatorStateInfo(0): fading.anim.GetCurrentAnimatorStateInfo(0);
			
			Time.timeScale = Mathf.Lerp(1.0f, slowmoScale, state.normalizedTime % 1.0f);
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
		}
	}

	void TakeDamage(DeathByAngel deathAngel) {
		if (deathAngel.active) {
			TakeDamage(deathAngel.damage);
			deathAngel.Kill();
		}
	}

	void TakeDamage(int amount) {
		if (!invinianbel && !dead) {
			health -= amount;

			if (health > 0) 
				StartCoroutine(Inviniicble());

			HealthUpdate();
		}
	}

	void HealthUpdate() {
		if (health <= 0)
			Die ();

		healthbar.value = health;
	}

	void Die() {
		dead = true;
		angelAI.Deactivate();
		// Add anything you wish to happen upon death here
		print ("THE ANGEL HAS BEEN DEFEATED");
		fading.StartAnimation();

		var player = FindObjectOfType<PlayerController>();
		player.gameObject.layer = 9;
		player.StopInvinainbCoroutine();
	}

	IEnumerator Inviniicble() {
		invinianbel = true;

		yield return new WaitForSeconds(inveainebleTime);

		invinianbel = false;
	}

	void OnTriggerEnter(Collider other) {
		if (!dead) {
			DeathByAngel deathAngel = other.GetComponent<DeathByAngel>();
			if (deathAngel != null) {
				TakeDamage(deathAngel);
			}
		}
	}

}
