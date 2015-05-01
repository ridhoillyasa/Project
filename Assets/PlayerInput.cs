using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvanceMovement))]
public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {/*
		if(Input.GetButtonUp("Toggle Inventory")){
			Messenger.Broadcast("ToggleInventory");
		}*/
		
		if(Input.GetButton("Vertical")){
			if(Input.GetAxis("Vertical")> 0){
				SendMessage("MoveMeForward", AdvanceMovement.Forward.forward);
			}else{
				SendMessage("MoveMeForward", AdvanceMovement.Forward.back);
			}
		}
		
		if(Input.GetButtonUp("Vertical")){
			SendMessage("MoveMeForward", AdvanceMovement.Forward.none);
		}
		
		if(Input.GetButton("Horizontal")){
			if(Input.GetAxis("Horizontal")> 0){
				SendMessage("TurnMe", AdvanceMovement.Turn.right);
			}else{
				SendMessage("TurnMe", AdvanceMovement.Turn.left);
			}
		}
		
		if(Input.GetButtonUp("Horizontal")){
			SendMessage("TurnMe", AdvanceMovement.Turn.none);
		}
		
		if(Input.GetButton("Strafe")){
			if(Input.GetAxis("Strafe")> 0){
				SendMessage("Strafe", AdvanceMovement.Turn.right);
			}else{
				SendMessage("Strafe", AdvanceMovement.Turn.left);
			}
		}
		
		if(Input.GetButtonUp("Strafe")){
			SendMessage("Strafe", AdvanceMovement.Turn.none);
		}
		
		if(Input.GetButton("Jump")){
			SendMessage("JumpUp");
		}
		
		if(Input.GetButtonDown("Run")){
			SendMessage("ToggleRun");
		}
	}
}
