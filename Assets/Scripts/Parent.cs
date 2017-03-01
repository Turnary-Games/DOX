using UnityEngine;
using System.Collections;

public class Parent : MonoBehaviour {

	public Transform parent;

	private Vector3 offset;

	void Start() {
		offset = transform.position - parent.position;
	}

	void LateUpdate() {
		transform.position = parent.position + offset;
	}

}