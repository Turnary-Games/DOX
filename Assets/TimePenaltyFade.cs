using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform),typeof(CanvasGroup))]
public class TimePenaltyFade : MonoBehaviour {

	public float time = 2;
	public float yMove = -50f;
	private float passed = 0;
	private float yStart;

	private RectTransform rect;
	private CanvasGroup canvasGroup;

	private void Start() {
		rect = transform as RectTransform;
		canvasGroup = GetComponent<CanvasGroup>();
		yStart = rect.anchoredPosition.y;
	}

	private void Update () {
		passed += Time.deltaTime / time;
		if (passed >= 1) {
			Destroy(gameObject);
		} else {
			rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, yStart + yMove * passed);
			canvasGroup.alpha = 1 - passed;
		}

	}
}
