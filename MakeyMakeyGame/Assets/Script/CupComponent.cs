using UnityEngine;
using System.Collections;

public enum AnimState
{
	Left,
	Right,
	None
}

public class CupComponent : MonoBehaviour
{
	protected KeyCode key;
	protected Vector3 shuffleLeft;
	protected Vector3 shuffleRight;
	protected Vector3 goTo = new Vector3();
	public GameObject cup;

	protected float speed = 10F;

	protected AnimState state;


	public int id;

	void Start() {
		state = AnimState.None;
	}


	void Update() {
		if(state == AnimState.Left){
			transform.position = Vector3.Lerp(transform.position, goTo, speed * Time.deltaTime);
			if(transform.position == goTo){
				state = AnimState.None;
			}
		}else if(state == AnimState.Right){
			transform.position = Vector3.Lerp(transform.position, goTo, speed * Time.deltaTime);
			if(transform.position == goTo){
				state = AnimState.None;
			}
		}

	}

	private void StartAnimation(AnimState dir){
		if(dir == AnimState.Left){
			state = AnimState.Left;
		}else if(dir == AnimState.Right){
			state = AnimState.Right;
		}
	}

	public void setShuffleLeft(Vector3 toLeft){
		goTo = toLeft;
		StartAnimation(AnimState.Left);
		cup.GetComponent<CupAnimator>().StartAnimation("left");
	}

	public void setShuffleRight(Vector3 toRight){
		goTo = toRight;
		StartAnimation(AnimState.Right);
		cup.GetComponent<CupAnimator>().StartAnimation("right");
	}

	public void setKey(KeyCode key){
		this.key = key;
	}

	public KeyCode getKey(){
		return key;
	}

	public AnimState getState(){
		return state;
	}

	public void setState(AnimState state){
		this.state = state;
	}

	private void setID(int id){
		this.id = id;
	}

}
