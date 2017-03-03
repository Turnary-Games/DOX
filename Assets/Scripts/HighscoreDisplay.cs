using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour {
	
	public Text text;
	public Viewing viewing = Viewing.NewHighscore;

	private void Awake() {
		ScoreTimer.LoadHighscore();
	}

	private void Start() {
		if (ScoreTimer.highscore < 0) { 
			// No score at all
			gameObject.SetActive(false);
		} else if (ScoreTimer.newHighscore == false) {
			if (ScoreTimer.oldScore > 0) {
				// View old highscore and old score
				if (viewing == Viewing.OldScore)
					text.text = ScoreTimer.FormatTime(ScoreTimer.oldScore);
				else if (viewing == Viewing.OldHighscore)
					text.text = ScoreTimer.FormatTime(ScoreTimer.highscore);
				else
					gameObject.SetActive(false);
			} else {
				// View old highscore
				if (viewing == Viewing.OldScore)
					gameObject.SetActive(false);
				else if (viewing == Viewing.OldHighscore) {
					text.text = ScoreTimer.FormatTime(ScoreTimer.highscore);
					var rect = transform as RectTransform;
					rect.pivot = new Vector2(0.5f, rect.pivot.y);
					rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y);
				} else
					gameObject.SetActive(false);
			}
		} else {
			// New highscore
			if (viewing == Viewing.OldScore)
				gameObject.SetActive(false);
			else if (viewing == Viewing.OldHighscore)
				gameObject.SetActive(false);
			else
				text.text = ScoreTimer.FormatTime(ScoreTimer.highscore);
		}
	}

	public enum Viewing {
		OldScore,
		OldHighscore,
		NewHighscore
	}
}
