using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Texture2D	startScreen;
	public string		title = "";
	public string		startGame = "";
	public string		quitGame = "";
	public Texture2D	menuBackground;

	public GUISkin		skin;
	public string		firstLevel;

	void OnGUI ()
	{
		GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), startScreen);
		GUI.skin = skin;

		GUI.BeginGroup (new Rect (Screen.width / 2 -50, Screen.height / 2 -175, 350, 350),menuBackground);
		GUI.Label (new Rect (50, 50, 170, 30),title);

		if ( GUI.Button (new Rect (80, 100, 100, 20), startGame))
		{
			SceneManager.LoadScene(firstLevel);

		}

		if (GUI.Button (new Rect (80, 130, 100, 20), quitGame))
		{
			Application.Quit ();
		}
		GUI.EndGroup ();
	}
}
