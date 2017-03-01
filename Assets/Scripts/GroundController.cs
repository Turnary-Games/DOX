using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	public GameObject destination;
	public float speed;

	void Update () {
		transform.Translate(Vector3.back * speed);

	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "trigger"){
			teleport();
		}
	}

	void teleport() {
		transform.position = destination.transform.position;
	}
}
