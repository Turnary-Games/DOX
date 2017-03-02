using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTimer : MonoBehaviour {
	
	private float start;
	private Text text;
	public static float highscore = -1;

	private void Awake() {
		text = GetComponent<Text>();
		LoadHighscore();
	}

	public void StartTimer() {
		start = Time.time;
		this.enabled = true;
	}

	public void StopTimer() {
		this.enabled = false;
		float passed = start - Time.time;
		text.text = FormatTime(passed);
		if (passed < highscore || highscore < 0) {
			highscore = passed;
			PlayerPrefs.SetFloat("highscore", highscore);
		}
	}

	public static void LoadHighscore() {
		if (highscore < 0)
			highscore = PlayerPrefs.GetFloat("highscore", -1);
	}

	private void Update() {
		float passed = Time.time - start;
		text.text = FormatTime(passed);
	}

	public static string FormatTime(float seconds) {
		int min = Mathf.FloorToInt(seconds/60) % 60;
		int sec = Mathf.FloorToInt(seconds) % 60;
		int centi = Mathf.FloorToInt(seconds * 100) % 100;

		return string.Format("{0} {1} {2}", min.ToString("D2"), sec.ToString("D2"), centi.ToString("D2"));
	}

}
