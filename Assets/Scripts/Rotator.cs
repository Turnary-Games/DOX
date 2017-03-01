using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	void Update () {
		if (gameObject.tag == "pickup")
			transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

		if (gameObject.tag == "bullet")
			transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime*10);

		if (gameObject.tag == "life")
			transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime*0.5f);
	}
}
