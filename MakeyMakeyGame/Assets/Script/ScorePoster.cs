using UnityEngine;
using System.Collections;

public class ScorePoster : MonoBehaviour {

	public bool enableUI = false;
	private bool buttonPressed;

	public int score;

	public string username = "Username"; 

	private void OnGUI(){
		if(enableUI){
			username = GUI.TextField(new Rect(Screen.width / 2 - 200 / 2, 150, 200, 30), username, 25);
			if(GUI.Button(new Rect(Screen.width / 2 - 100 / 2, 185, 100, 30), "Submit") && !buttonPressed){
				buttonPressed = true;
				StartCoroutine(SubmitScore());
			}
		}
	}

	private IEnumerator SubmitScore()
	{
		string post_url = "http://daanruiter.net/games/makeymakey/submit.php?" +
			"username=" + WWW.EscapeURL(username) +
				"&score=" + score;
		
		Debug.Log (post_url);
		
		WWW hs_post = new WWW(post_url);
		yield return hs_post; 
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
		
		Application.LoadLevel(2);
	}
}
