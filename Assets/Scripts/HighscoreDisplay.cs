using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour {

	public Text text;

	private void Awake() {
		ScoreTimer.LoadHighscore();
	}

	private void Start() {
		if (ScoreTimer.highscore < 0)
			gameObject.SetActive(false);
		else {
			text.text = ScoreTimer.FormatTime(ScoreTimer.highscore);
		}
	}
}
