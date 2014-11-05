using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShuffleState
{
	Shuffling,
	None,
	Init
}

public class GameController : MonoBehaviour {

	private List<GameObject> cups = new List<GameObject>();
	private List<KeyCode> keys = new List<KeyCode>(); 

	private int cup1;
	private int cup2;

	public int amountOfShuffles;

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

		//the keys to be used for each cup
		keys.Add(KeyCode.LeftArrow);
		keys.Add(KeyCode.DownArrow);
		keys.Add(KeyCode.RightArrow);
		keys.Add(KeyCode.UpArrow);

		//give each cup its key
		for(int i = 0; i < keys.Count; i++){
			cups[i].GetComponent<CupComponent>().setKey(keys[i]);
		}

		Invoke("Init", 2);

	}

	private void Init(){
		this.state = ShuffleState.None;
	}

	private void Update(){
		GetInput(); //check the keys begin pressed
		if(state == ShuffleState.None && shuffles < amountOfShuffles){
			ShuffleCups();
		}
		if(state == ShuffleState.Shuffling){
			if(cups[cup1].GetComponent<CupComponent>().getState() == AnimState.None){
				if(cups[cup2].GetComponent<CupComponent>().getState() == AnimState.None){
					state = ShuffleState.None;
				}
			}
		}
	}

	private void GetInput(){
	}

	private void ShuffleCups(){
		cup1 = Random.Range(0, cups.Count - 1);
		if(cup1 == cups.Count)
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

}