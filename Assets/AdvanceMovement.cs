using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AdvanceMovement : MonoBehaviour {
	public enum State{
		Idle,
		Init,
		Setup,
		Run
	}
	public enum Turn{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward{
		back = -1,
		none = 0,
		forward = 1
	}
	public float turnSpeed = 250.0f;				//kecepatan berbelok karakter
	public float walkSpeed = 2.0f;					//kecepatan berjalan karakter
	public float runMultiplier = 3.0f;
	public float strafeSpeed = 2.5f;				//kecepatan berjalan kesamping karakter
	public float gravity = 20.0f;					//setingan untuk grafitasi
	public float airTime = 0;						//berapa lama karakter berada di udara setelah terakhir kali menyentuh tanah
	public float fallTime = .5f;					//waktu jatuh karakter
	public float jumpHight = 8.0f;					//tinggi lompatan
	public float jumpTime = 1.5f;					//lama lompatan
	
	public CollisionFlags collisionFlags;			//collisionFlags yang ada dari frame terakhir
	
	private Vector3 _moveDirection;					//arah karakter kita bergerak
	private Transform _myTransform;
	private CharacterController _charCtrl;
	
	private Turn _turn;
	private Forward _fwd;
	private Turn _strafe;
	private bool _run;
	private bool _jump;
	
	private State _state;
	
	public void Awake(){
		_myTransform = transform;
		_charCtrl = GetComponent<CharacterController>();
		
		_state = AdvanceMovement.State.Init;
	}
	
	// Use this for initialization
	IEnumerator Start () {
		while(true){
			switch(_state){
			case State.Init:
				Init();
				break;
			case State.Setup:
				SetUp();
				break;
			case State.Run:
				ActionPicker();
				break;
			}
			yield return null;
		}
	}
	
	// Update is called once per frame
	private void Init(){
		if(!GetComponent<CharacterController>())return;
		if(!GetComponent<Animation>())return;
		
		_state = AdvanceMovement.State.Setup;
	}
	
	private void SetUp(){
		_moveDirection = Vector3.zero;
		
		animation.Stop();
		animation.wrapMode = WrapMode.Loop;
		animation["Jump"].layer = 1;
		animation["Jump"].wrapMode = WrapMode.Once;
		animation.Play("Idle01");
		
		_turn = AdvanceMovement.Turn.none;
		_fwd = AdvanceMovement.Forward.none;
		_strafe = Turn.none;
		_run = false;
		_jump = false;
		
		_state = AdvanceMovement.State.Run;
	}
	
	private void ActionPicker(){
			_myTransform.Rotate(0, (int)_turn * Time.deltaTime * turnSpeed, 0);

		if(_charCtrl.isGrounded){
			//Debug.Log("On the ground");
			airTime = 0;
			
			_moveDirection = new Vector3((int)_strafe, 0, (int)_fwd);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(_fwd != Forward.none){
				if(_run){
					_moveDirection *= runMultiplier;
					Run();
				}else{
					Walk();
				}
			}else if(_strafe != AdvanceMovement.Turn.none){
				Strafe();
			}else{
				Idle();
			}
			
			if(_jump){
				if(airTime < jumpTime){
					_moveDirection.y += jumpHight;
					Jump();
				}
			}
		}else{
			//Debug.Log("Not On the ground");
			
			if((collisionFlags & CollisionFlags.CollidedBelow) == 0){
				airTime += Time.deltaTime;
				
				if(airTime > fallTime){
					Fall();
					_jump = false;
				}
			}
		}
		_moveDirection.y -= gravity * Time.deltaTime;
		collisionFlags = _charCtrl.Move(_moveDirection * Time.deltaTime);
	}
	

	public void MoveMeForward(Forward z){
		_fwd = z;
	}
	
	public void ToggleRun(){
		_run = !_run;
	}
	
	public void TurnMe(Turn y){
		_turn = y;
	}
	
	public void Strafe(Turn x){
		_strafe = x;
	}
	
	public void JumpUp(){
		_jump = true;
	}
	
	public void Idle(){
		animation.CrossFade("Idle01");
	}
	
	public void Fall(){
		animation.CrossFade("Fall");
	}
	
	public void Walk(){
		animation.CrossFade("Walk");
	}
	
	public void Run(){
		animation.CrossFade("Run");
	}
	
	public void Strafe(){
		animation.CrossFade("SideRight");
	}
	
	public void Jump(){
		animation.CrossFade("Jump");
	}
}
