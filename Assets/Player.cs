using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private delegate void UpdateDelegate();
	private UpdateDelegate updateDelegate;

	public AnimationClip idle;
	public AnimationClip walk;
	public AnimationClip slash;

	private enum CharacterState {
		Idle       	= 0,
		Walking 	= 1,
		Slash		= 2,
		DoNothing   = 3
	}
	private CharacterState _characterState;

	private void Awake () 
	{
		updateDelegate = DoNothing;
	}
	private void Start () 
	{
		animation.CrossFade(idle.name);
		updateDelegate = UpdateAnimation;
	}
	
	private void Update () 
	{
		updateDelegate();
	}

	private void UpdateAnimation()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			_characterState = CharacterState.Slash;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			_characterState = CharacterState.Walking;
		}
		else
		{
			_characterState = CharacterState.Idle;
		}

		if(_characterState == CharacterState.Slash )
		{
			animation.CrossFade(slash.name);

			StopAllCoroutines();
			StartCoroutine(Slashing());
			updateDelegate = DoNothing;
		}
		else if(_characterState == CharacterState.Walking )
		{
			animation.CrossFade(walk.name);
		}
		else
		{
			animation.CrossFade(idle.name);
		}
	}

	private IEnumerator Slashing()
	{
		_characterState = CharacterState.DoNothing;
		while (animation.isPlaying)
		{ 
			yield return null; 
		}

		StopAllCoroutines();
		updateDelegate = UpdateAnimation;
	}

	private void DoNothing()
	{
		
	}

}
