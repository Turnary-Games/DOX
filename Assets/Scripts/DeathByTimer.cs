using UnityEngine;
using System.Collections;

public class DeathByTimer : MonoBehaviour {

	public float timer = 1;

	void Start () {

	}

	void DestroyGameObject() {
		Destroy(gameObject);
	}

	public void SetTimer(float time) {
		CancelTimer();
		Invoke("DestroyGameObject", time);
	}

	public void CancelTimer() {
		CancelInvoke("DestroyGameObject");
	}

	public void ResetTimer() {
		SetTimer(timer);
	}

}
