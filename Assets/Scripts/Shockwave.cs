using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour {

	// Called from within the animation
	public void DestroyGameObject() {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "bullet") {
			Rigidbody rbody = other.attachedRigidbody;

			// Go towards the angel
			Vector3 angel = GameObject.FindGameObjectWithTag("Angel bullet target").transform.position;
			Vector3 diff = angel - transform.position;

			// velocity = direction * original velocity speed
			// in this case its velocity is set to away from this object
			rbody.velocity = diff.normalized * rbody.velocity.magnitude;
			
			DeathByTimer deathTimer = rbody.GetComponent<DeathByTimer>();
			deathTimer.ResetTimer();

			DeathByAngel deathAngel = rbody.GetComponent<DeathByAngel>();
			deathAngel.active = true;
		}
	}

}
