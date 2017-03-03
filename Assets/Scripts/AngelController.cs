using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngelController : MonoBehaviour {

	public AngelAI angelAI;
	public int health = 1;
	[Space(12)]
	public Slider healthbar;
	public ChangeScene fading;
	public GameObject explosionPrefab;
	[Space(12)]
	public float slowmoScale;
	public float slowmoSpeed;
	
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
			var clone = Instantiate(explosionPrefab, deathAngel.transform.position, Quaternion.identity);
			clone.transform.localScale = Vector3.one * Mathf.Max(1, deathAngel.damage * 0.3f);
			deathAngel.Kill();
		}
	}

	void TakeDamage(int amount) {
		if (!dead) {
			health -= amount;

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

	void OnTriggerEnter(Collider other) {
		if (!dead) {
			DeathByAngel deathAngel = other.GetComponent<DeathByAngel>();
			if (deathAngel != null) {
				TakeDamage(deathAngel);
			}
		}
	}

}
