using UnityEngine;
using System.Collections;

public class SkyColour : MonoBehaviour {
	
	private Camera cam;
	private Color a;
	public Color b;
	public Color c;
	public Light mainlight;
	public Light filllight;

	void Start () {
		cam = GetComponent<Camera> ();
		a = cam.backgroundColor;
	}

	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectsWithTag ("pickup").Length == 0) {
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, c, Time.deltaTime);
			mainlight.intensity = Mathf.Lerp (mainlight.intensity, 0, Time.deltaTime);
			filllight.intensity = Mathf.Lerp (filllight.intensity, 1, Time.deltaTime);
		}

		else
			cam.backgroundColor = Color.Lerp (a, b, Mathf.Cos (Time.time) / 2 + .5f);
	}
}
