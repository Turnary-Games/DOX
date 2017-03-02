using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public string nextScene;
	[HideInInspector] public Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	public void StartAnimation() {
		anim.SetTrigger("Reverse");
	}

	public void StartAnimation(string scene) {
		nextScene = scene;
		anim.SetTrigger("Reverse");
	}

	// Called from within the "reverse fade" animation
	public void LoadScene() {
		Time.timeScale = 1;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
		SceneManager.LoadScene(nextScene);
	}

}
