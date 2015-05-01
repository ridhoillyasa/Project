using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement1 : MonoBehaviour {
	public float rotateSpeed = 250.0f;
	public float moveSpeed = 5.0f;
	public float strafeSpeed = 2.5f;
	
	private Transform _myTransform;
	private CharacterController _charCtrl;
	
	public void Awake(){
		_myTransform = transform;
		_charCtrl = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if(!_charCtrl.isGrounded){
			_charCtrl.Move(Vector3.down * Time.deltaTime);
		}
		Turn();
		Walk();
		Strafe();
	}
	
	private void Turn(){
		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0){
//			Debug.Log("Rotate: " + Input.GetAxis("Horizontal"));
			_myTransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0);
		}
	}
	
	private void Walk(){
		if(Mathf.Abs(Input.GetAxis("Vertical")) > 0){
//			Debug.Log("Forward: " + Input.GetAxis("Vertical"));
			animation.CrossFade("Run");
			_charCtrl.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * moveSpeed);
		}else{
			animation.CrossFade("Idle");
		}
	}
	
	private void Strafe(){
		if(Mathf.Abs(Input.GetAxis("Strafe")) > 0){
//			Debug.Log("Strafe: " + Input.GetAxis("Strafe"));
			_charCtrl.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * strafeSpeed);
		}
	}
}
