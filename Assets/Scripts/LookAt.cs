using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	public Transform target;

	void LateUpdate () {
		transform.LookAt (target);
	}
}
