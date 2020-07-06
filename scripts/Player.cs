using Godot;
using System;

public class Player : KinematicBody
{
	public float MOVE_SPEED = 12;
	public float JUMP_FORCE = 30;
	public float GRAVITY = 0.98F;
	public float MAX_FALL_SPEED = 30;
	public float H_LOOK_SENS = 0.2F;
	public float V_LOOK_SENS = 0.2F;
	public float y_velo = 0;
	private Spatial cube;
	private bool just_jumped=false;
	private Control pause_menu;
	private Spatial cam;
	private Sprite joystick;
	private Vector2 joystick_value = new Vector2(0,0);

	public bool paused = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cam = (Spatial)GetNode("CamBase");
		cube = (Spatial)GetNode("cube");
		joystick = (Sprite)GetNode("joystick");
		joystick.Connect("get_value", this, nameof(onJoystickValue));

		String os_name = OS.GetName();
		GD.Print("os name : "+os_name);
	}

	public void onJoystickValue(Vector2 value){
		//GD.Print("Player value getted : ",value);
		joystick_value=value;
		//GD.Print(joystick_value);
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion && !paused){
			Vector3 rot_deg=cam.RotationDegrees;
			InputEventMouseMotion ie=(InputEventMouseMotion)@event;
			rot_deg.x -= ie.Relative.y * V_LOOK_SENS;
			if(rot_deg.x < -90){ rot_deg.x=-90; }
			if(rot_deg.x > 90){ rot_deg.x=90; }
			rot_deg.y -= ie.Relative.x * H_LOOK_SENS;
			cam.RotationDegrees=rot_deg;
		}
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if(!paused){
			Vector3 v3;
			v3.x=0;
			v3.y=1;
			v3.z=0;
			//
			Vector3 move_vec;
			move_vec.x=0;
			move_vec.y=0;
			move_vec.z=0;
			if(Input.IsActionPressed("move_forward") || joystick_value.y<=-0.8){
				move_vec.z -= 1;
			}
			if(Input.IsActionPressed("move_backward") || joystick_value.y>=0.8){
				move_vec.z += 1;
			}
			if(Input.IsActionPressed("move_left") || joystick_value.x<=-0.8){
				move_vec.x -= 1;
			}
			if(Input.IsActionPressed("move_right") || joystick_value.x>=0.8){
				move_vec.x += 1;
			}
			move_vec=move_vec.Normalized();

			move_vec=move_vec.Rotated(new Vector3(0,1,0), cam.RotationDegrees.y*3.141592654F/180.0F);
			move_vec *= MOVE_SPEED;

			if(move_vec.Length() >= 0.1F){
				Vector3 rot_deg=cube.RotationDegrees;
				rot_deg.y=cam.RotationDegrees.y;
				cube.RotationDegrees=rot_deg;
			}

			just_jumped=false;
			bool grounded=IsOnFloor();
			y_velo -= GRAVITY;
			
			if(grounded && Input.IsActionPressed("jump")){
				just_jumped=true;
				y_velo=JUMP_FORCE;
			}
			if(grounded && y_velo<=0){
				y_velo = -0.1F;
			}
			if(y_velo < -MAX_FALL_SPEED){
				y_velo=-MAX_FALL_SPEED;
			}
			move_vec.y=y_velo;

			MoveAndSlide(move_vec, v3);
		}

	}

}
