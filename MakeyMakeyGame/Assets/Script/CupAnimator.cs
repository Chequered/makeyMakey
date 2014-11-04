using UnityEngine;
using System.Collections;

public class CupAnimator : MonoBehaviour
{
	public AnimationClip[] slideClips;

	private bool _playing;

	public void StartAnimation(string side){
		if(side == "left"){
			animation.clip = slideClips[0];
			animation.Play();
		}else if(side == "right"){
			animation.clip = slideClips[1];
			animation.Play();
		}
	}

}

