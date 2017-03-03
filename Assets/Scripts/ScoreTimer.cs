using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTimer : MonoBehaviour {

	private float extra;
	private float start;
	private Text text;
	public static float highscore = -1;
	private int speedMultiplier;
	public Image fade;

	private void Awake() {
		text = GetComponent<Text>();
		LoadHighscore();
		speedMultiplier = 1;
	}

	public void StartTimer() {
		start = Time.time;
		extra = 0;
		this.enabled = true;
	}

	public void SpeedUp() {
		if (speedMultiplier < 4 && this.enabled) {
			extra = (Time.time - start) * speedMultiplier + extra;
			start = Time.time;
			
			fade.canvasRenderer.SetAlpha(1);
			fade.CrossFadeAlpha(0, 1, false);

			speedMultiplier += 1;
		}
	}

	public void StopTimer(bool saveHighscore) {
		if (this.enabled) {
			this.enabled = false;
			float passed = (Time.time - start) * speedMultiplier + extra;
			text.text = FormatTime(passed);
			if (saveHighscore) {
				if (passed < highscore || highscore < 0) {
					highscore = passed;
					PlayerPrefs.SetFloat("highscore", highscore);
				}
			}
		}
	}

	public static void LoadHighscore() {
		if (highscore < 0)
			highscore = PlayerPrefs.GetFloat("highscore", -1);
	}

	private void Update() {
		float passed = (Time.time - start) * speedMultiplier + extra;
		text.text = FormatTime(passed, speedMultiplier);
	}

	public static string FormatTime(float seconds, int speedMultiplier = 1) {
		int min = Mathf.FloorToInt(seconds/60) % 60;
		int sec = Mathf.FloorToInt(seconds) % 60;
		int centi = Mathf.FloorToInt(seconds * 100) % 100;

		if (speedMultiplier <= 1)
			return string.Format("{0} {1} {2}", min.ToString("D2"), sec.ToString("D2"), centi.ToString("D2"));
		else
			return string.Format("{0} {1} {2} x{3}", min.ToString("D2"), sec.ToString("D2"), centi.ToString("D2"), speedMultiplier.ToString("D1"));
	}

}
