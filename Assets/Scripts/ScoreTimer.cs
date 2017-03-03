using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour {

	public static float highscore = -1;
	public static float oldScore = -1;
	public static bool newHighscore;

	public Image fade;
	public Text text;
	public float timePenalty = 5;
	public GameObject timePenaltyPrefab;

	private float extra = 0;
	private float start;

	private void Awake() {
		LoadHighscore();
	}

	public void StartTimer() {
		start = Time.time;
		oldScore = -1;
		this.enabled = true;
	}

	public void AddTimePenalty() {
		if (this.enabled) {
			extra = (Time.time - start) + extra + timePenalty;
			start = Time.time;
		} else
			extra += timePenalty;

		fade.color = Color.white;
		fade.canvasRenderer.SetAlpha(1);
		fade.CrossFadeAlpha(0, 1, false);

		if (!this.enabled)
			Update();

		// Spawn prefab
		var clone = Instantiate(timePenaltyPrefab);
		clone.transform.SetParent(transform.parent, false);
		(clone.transform as RectTransform).anchoredPosition = (timePenaltyPrefab.transform as RectTransform).anchoredPosition;
	}

	public void StopTimer(bool saveHighscore) {
		if (this.enabled) {
			this.enabled = false;
			oldScore = (Time.time - start) + extra;
			text.text = FormatTime(oldScore);
			if (saveHighscore) {
				if (oldScore < highscore || highscore < 0) {
					highscore = oldScore;
					PlayerPrefs.SetFloat("highscore", highscore);
					newHighscore = true;
				} else
					newHighscore = false;
			}
		}
	}

	public static void LoadHighscore() {
		if (highscore < 0)
			highscore = PlayerPrefs.GetFloat("highscore", -1);
	}

	private void Update() {
		if (this.enabled) {
			float passed = (Time.time - start) + extra;
			text.text = FormatTime(passed);
		} else {
			text.text = FormatTime(extra);
		}
	}

	public static string FormatTime(float seconds) {
		int min = Mathf.FloorToInt(seconds/60) % 60;
		int sec = Mathf.FloorToInt(seconds) % 60;
		int centi = Mathf.FloorToInt(seconds * 100) % 100;
		
		return string.Format("{0} {1} {2}", min.ToString("D2"), sec.ToString("D2"), centi.ToString("D2"));
	}

}
