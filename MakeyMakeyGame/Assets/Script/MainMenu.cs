using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width / 2 - 100 / 2, 185, 100, 30), "Start")){
			Application.LoadLevel(0);
		}
	}
}
