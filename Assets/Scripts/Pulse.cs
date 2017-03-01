using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float speed;

	void Start() {
		SetSize (0);
	}

	void Update () {
		AddSize (Time.deltaTime * speed);
	}

	void SetSize(float value) {
		transform.localScale = new Vector3 (value, transform.localScale.y, value);
	}

	void AddSize(float value) {
		SetSize (transform.localScale.x + value);
	}
}
