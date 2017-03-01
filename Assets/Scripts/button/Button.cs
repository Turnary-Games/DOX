using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
	
	public Type buttonType;
	public string nextScene;
	[HideInInspector]
	public bool hover;
	
	public void Activate() {
		
		if (buttonType == Type.loadlevel) {
			Time.timeScale = 1;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			SceneManager.LoadScene(nextScene);
		}
		
		if (buttonType == Type.exit) {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}

	void Update() {
		Vector3 target = Vector3.one * (hover ? 1.2f : 1);
		transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * 5f);
	}
	
	public enum Type {
		exit, loadlevel
	};
}

