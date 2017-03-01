using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour {

	public LayerMask buttonMask;
	private Camera cam;
	private AudioSource source;

	void Start(){
		source = GetComponent<AudioSource> ();
		cam = GetComponent<Camera> ();
	}
	
	void Update() {
		foreach (Button button in FindObjectsOfType<Button>()) {
			button.hover = false;
		}

		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, buttonMask)) {

			if (hit.collider != null)  {
				Button button = hit.collider.GetComponent<Button>();

				if (button != null) {
					button.hover = true;

					if (Input.GetMouseButtonUp(0)) {
						// Klickade på en knapp
						button.Activate();
						
						source.Play();
					
					}
				}
			}
				

		}
	}
	
}
