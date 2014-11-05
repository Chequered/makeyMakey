using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShuffleState
{
	Shuffling,
	None,
	Init
}

public enum Result
{
	Win,
	Lose,
	Playing
}

public class GameController : MonoBehaviour {

	private List<GameObject> cups = new List<GameObject>();
	private List<KeyCode> keys = new List<KeyCode>(); 

	private int cup1;
	private int cup2;
	private bool smashingEnabled;
	private int points = 2000;
	private float cupWithNoSpike;
	private bool smashed;
	private Result result = Result.Playing;

	public int amountOfShuffles;
	public GameObject hitItText;
	public GameObject scoreText;
	public GameObject scoreSubmitter;

	private int shuffles;

	private ShuffleState state;

	private void Start(){
		//get all the cups in the scene
		GameObject[] sceneCups = GameObject.FindGameObjectsWithTag("cup");

		this.shuffles = 0;
		this.state = ShuffleState.Init;

		//add the cups in the scene to the List
		for(int i = 0; i < sceneCups.Length; i++){
			if(sceneCups[i].GetComponent<CupComponent>()){
				cups.Add(sceneCups[i]);
				sceneCups[i].GetComponent<CupComponent>().id = i;
			}else{
				Debug.LogError("You need to add a Cup coponent script to " + sceneCups[i]);
			}
		}

		int noSpike = Random.Range(0, cups.Count - 1);
		cups[noSpike].GetComponent<CupComponent>().spike.renderer.enabled = false;

		//the keys to be used for each cup


		//give each cup its key
		for(int i = 0; i < cups.Count; i++){
			cups[i].GetComponent<CupComponent>().cup.animation.Play("animation_cup_flash");
		}

		//wait for the animations to finish
		Invoke("Init", 4f);

	}

	private void Init(){
		this.state = ShuffleState.None;
		for(int i = 0;i < cups.Count; i++){
			cups[i].GetComponent<CupComponent>().spike.transform.parent = cups[i].GetComponent<CupComponent>().cup.transform;
		}
	}

	private void Update(){
		GetInput(); //check the keys begin pressed
		if(state == ShuffleState.None && shuffles < amountOfShuffles){
			ShuffleCups();
		}else if(shuffles >= amountOfShuffles){
			Invoke("EnableHitting", 0.8f);
		}
		if(state == ShuffleState.Shuffling){
			if(cups[cup1].GetComponent<CupComponent>().getState() == AnimState.None){
				if(cups[cup2].GetComponent<CupComponent>().getState() == AnimState.None){
					state = ShuffleState.None;
				}
			}
		}

		if(smashingEnabled){
			scoreText.GetComponent<TextMesh>().text = "Score: " + points;
			if(points >= 15 && !smashed){
				points -= 15;
			}
		}
	}

	private void GetInput(){
		if(shuffles >= amountOfShuffles){
			if(Input.GetKey(KeyCode.LeftArrow) && !smashed){
				if(cupWithNoSpike == 0){
					result = Result.Win;
					GetComponent<ScorePoster>().enableUI = true;
					GetComponent<ScorePoster>().score = points;
				}else{
					result = Result.Lose;
				}
				smashed = true;
			}
			if(Input.GetKey(KeyCode.DownArrow) && !smashed){
				if(cupWithNoSpike == 2){
					result = Result.Win;
					GetComponent<ScorePoster>().enableUI = true;
					GetComponent<ScorePoster>().score = points;
				}else{
					result = Result.Lose;
				}
				smashed = true;
			}
			if(Input.GetKey(KeyCode.RightArrow) && !smashed){
				if(cupWithNoSpike == 4){
					result = Result.Win;
					GetComponent<ScorePoster>().enableUI = true;
					GetComponent<ScorePoster>().score = points;
				}else{
					result = Result.Lose;
				}
				smashed = true;
			}
			if(Input.GetKey(KeyCode.UpArrow) && !smashed){
				if(cupWithNoSpike == 6){
					result = Result.Win;
					GetComponent<ScorePoster>().enableUI = true;
					GetComponent<ScorePoster>().score = points;
				}else{
					result = Result.Lose;
				}
				smashed = true;
			}
		}
	}

	private void ShuffleCups(){
		cup1 = Random.Range(0, cups.Count - 1);
		if(cup1 == cups.Count - 1)
		{
			cup2 = cup1 - 1;
		}else{
			cup2 = cup1 + 1;
		}
		if(cup1 > cup2){
			//if cup1 is not on the most left, and cup 1 is right of cup2
			cups[cup1].GetComponent<CupComponent>().setShuffleLeft(cups[cup2].transform.position);
			cups[cup2].GetComponent<CupComponent>().setShuffleRight(cups[cup1].transform.position);
			cups[cup1].GetComponent<CupComponent>().setState(AnimState.Left);
			cups[cup2].GetComponent<CupComponent>().setState(AnimState.Right);
		}else{
			//if cup1 is not on the most left, and cup 1 is right of cup2
			cups[cup2].GetComponent<CupComponent>().setShuffleLeft(cups[cup1].transform.position);
			cups[cup1].GetComponent<CupComponent>().setShuffleRight(cups[cup2].transform.position);
			cups[cup1].GetComponent<CupComponent>().setState(AnimState.Right);
			cups[cup2].GetComponent<CupComponent>().setState(AnimState.Left);
		}

		state = ShuffleState.Shuffling;
		shuffles++;
	}

	private void EnableHitting(){
		hitItText.renderer.enabled = true;
		scoreText.renderer.enabled = true;
		smashingEnabled = true;
		for(int i = 0; i < cups.Count; i++){
			if(!cups[i].GetComponent<CupComponent>().hasSpike()){
				cupWithNoSpike = cups[i].transform.localPosition.z;
			}
		}
	}

}