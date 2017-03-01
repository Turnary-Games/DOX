using UnityEngine;
using System.Collections;

public class DeathByAngel : MonoBehaviour {

	[HideInInspector] public bool active;
	public int damage = 1;

	public void Kill() {
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		GetComponent<TrailRenderer>().autodestruct = true;
		GetComponent<MeshRenderer>().enabled = false;
	}

}
