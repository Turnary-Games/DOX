using UnityEngine;
using System.Collections;

public class Permanent : MonoBehaviour {

	static Permanent instance;

	void Awake() {
		if (instance == null)
			instance = this;

		if (instance != this)
			Destroy(gameObject);
		else
			DontDestroyOnLoad(gameObject);
		
	}
	
}
