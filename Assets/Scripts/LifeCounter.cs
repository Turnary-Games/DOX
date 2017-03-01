using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour {

	private Material original;
	public Material material1;
	public int lifeLimit;

	void Start() {
		original = GetComponent<MeshRenderer>().material;
		LifeUpdate();
	}

	public void LifeUpdate() {
		if (FindObjectOfType<PlayerController>().lives >= lifeLimit)
			GetComponent<MeshRenderer>().material = material1;
		else
			GetComponent<MeshRenderer>().material = original;
	}

	
	//GetComponent<MeshRenderer>().material = 
}
