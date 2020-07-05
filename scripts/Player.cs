using Godot;
using System;

public class Player : KinematicBody
{
	public float MOVE_SPEED = 12;
	public float JUMP_FORCE = 30;
	public float GRAVITY = 0.98;
	public float MAX_FALL_SPEED = 30;
	public float H_LOOK_SENS = 1.0;
	public float V_LOOK_SENS = 1.0;
	public float y_velo = 0;

	private Spatial cam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cam = (Spatial)GetNode("CamBase");
	}

	public override void _Input(Input Event)
	{
		if(Event is InputEventMouseMotion){
			cam.rotation_degrees.x -= Event.relative.y * V_LOOK_SENS;
			cam.rotation_degrees.x = Math.Clamp(cam.rotation_degrees.x, -90,90);
			rotation_degrees.y -= Event.relative.x * H_LOOK_SENS; 
		}
	}

	public override void _PhysicProcess(float delta)
	{
		move_vec=Vector3();
		if(Input.is_action_pressed("move_forward")){
			move_vec.z -= 1;
		}
		if(Input.is_action_pressed("move_backward")){
			move_vec.z += 1;
		}
		if(Input.is_action_pressed("move_left")){
			move_vec.x -= 1;
		}
		if(Input.is_action_pressed("move_right")){
			move_vec.x += 1;
		}
		move_vec=move_vec.normalized();
		move_vec=move_vec.rotated(Vector3(0,1,0), rotation.y);
		move_vec *= MOVE_SPEED;
		move_and_slide(move_vec, Vector3(0, 1, 0));

		bool grounded=isOnFloor();
		y_velo -= GRAVITY;
		bool just_jumped = false;
		if(grounded && _Input.is_action_pressed("jump")){
			just_jumped=true;
			y_velo=JUMP_FORCE;
		}
		if(grounded && y_velo<=0){
			y_velo = -0.1;
		}
		if(y_velo < -MAX_FALL_SPEED){
			y_velo=-MAX_FALL_SPEED;
		}

	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
