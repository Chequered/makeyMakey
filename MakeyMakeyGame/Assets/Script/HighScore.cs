using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	private string scores;
	private bool done;

	public GUIStyle style;

	// Use this for initialization
	void Start () {
		StartCoroutine(GetScores());
	}

	private void OnGUI(){
		if(done){
			GUI.TextArea(new Rect(Screen.width / 2 - 400 / 2, 50, 400, 400), scores, style);
			if(GUI.Button(new Rect(Screen.width / 2 + 260, 400, 100, 50), "Continue")){
				Application.LoadLevel(1);
			}
		}
	}

	
	private IEnumerator GetScores() {
		WWW www = new WWW("http://daanruiter.net/games/makeymakey/getScores.php");
		yield return www;

		Debug.Log (www.url);

		if (www.error != null)
		{
			print("There was an error retrieving the scores: " + www.error);
		}
		
		scores = "Scores: \n" + www.text;
		done = true;
	}
}
