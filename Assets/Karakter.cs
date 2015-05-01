using UnityEngine;
using System.Collections;

public class Karakter : MonoBehaviour {
	
	private delegate void UpdateDelegate();
	private UpdateDelegate updateDelegate;
	
	public AnimationClip idle;
	public AnimationClip run;
	public AnimationClip left;
	public AnimationClip right;
	public AnimationClip back;
	public AnimationClip strafeleft;
	public AnimationClip straferight;
	public AnimationClip jump;
	public AnimationClip hack;
	public AnimationClip slash;

	
	private enum CharacterState {
		Idle       	= 0,
		Run		 	= 1,
		Left		= 2,
		Right 		= 3,
		Back 		= 4,
		StrafeLeft	= 5,
		StrafeRight	= 6,
		Jump		= 7,
		Hack		= 8,
		Slash		= 9,
		DoNothing   = 10
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
		if(Input.GetKey(KeyCode.W))
		{
			_characterState = CharacterState.Run;
		}
		else if(Input.GetKey(KeyCode.A))
		{
			_characterState = CharacterState.Left;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			_characterState = CharacterState.Right;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			_characterState = CharacterState.Back;
		}
		else if(Input.GetKey(KeyCode.Q))
		{
			_characterState = CharacterState.StrafeLeft;
		}
		else if(Input.GetKey(KeyCode.E))
		{
			_characterState = CharacterState.StrafeRight;
		}
		else if(Input.GetKey(KeyCode.Space))
		{
			_characterState = CharacterState.Jump;
		}
		else if(Input.GetKey(KeyCode.J))
		{
			_characterState = CharacterState.Hack;
		}
		else if(Input.GetKey(KeyCode.K))
		{
			_characterState = CharacterState.Slash;
		}
		else
		{
			_characterState = CharacterState.Idle;
		}
		
		if(_characterState == CharacterState.Run )
		{
			animation.CrossFade(run.name);
		}
		else if(_characterState == CharacterState.Slash )
		{
			animation.CrossFade(slash.name);

			StopAllCoroutines();
			StartCoroutine(Slashing());
			updateDelegate = DoNothing;
		}
		else if(_characterState == CharacterState.Left )
		{
			animation.CrossFade(left.name);
		}
		else if(_characterState == CharacterState.Right )
		{
			animation.CrossFade(right.name);
		}
		else if(_characterState == CharacterState.Back )
		{
			animation.CrossFade(back.name);
		}
		else if(_characterState == CharacterState.StrafeLeft )
		{
			animation.CrossFade(strafeleft.name);
		}
		else if(_characterState == CharacterState.StrafeRight )
		{
			animation.CrossFade(straferight.name);
		}
		else if(_characterState == CharacterState.Jump )
		{
			animation.CrossFade(jump.name);
		}
		else if(_characterState == CharacterState.Hack )
		{
			animation.CrossFade(hack.name);
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
