using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Jukebox : MonoBehaviour {

	public AudioSource source;

	private void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		if (scene.buildIndex == 0 && !source.isPlaying)
			source.Play();
	}
}
