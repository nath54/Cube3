using System.Threading;
using Godot;
using System;

public class Player : KinematicBody
{
	public float hauteur_min = -40;
	public float MOVE_SPEED = 12;
	public float JUMP_FORCE = 30;
	public float GRAVITY = 0.98F;
	public float MAX_FALL_SPEED = 30;
	public float H_LOOK_SENS = 0.2F;
	public float V_LOOK_SENS = 0.2F;
	public float y_velo = 0;
	private Spatial cube;
	private Control pause_menu;
	private Spatial cam;
	public TouchScreenButton joystick;
	public Vector3 spawnpoint;
	public CollisionShape cubeshape;
	private bool just_jumped=false;
	private bool is_joy_pressed = false;
	private bool is_joy_cam_pressed = false;
	public bool paused = false;
	public bool is_bt_jump_press = false;

	[Signal]
    public delegate void pause_bt_press();

	public bool is_mobile(){
		return (OS.GetName()=="Android" || OS.GetName()=="iOS");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cam = (Spatial)GetNode("CamBase");
		cube = (Spatial)GetNode("cube");
		cubeshape = (CollisionShape)GetNode("CollisionShape");
		joystick = (TouchScreenButton)GetNode("joystick/Joystick_Button");

		if( is_mobile() ){
		}
		else{
			
		}
		//
		spawnpoint=Translation;
		spawnpoint.y-=0.05F;
		//
	}



	public override void _Input(InputEvent @event)
	{
		if(!paused){
			if(is_mobile()){
				if(@event is InputEventScreenDrag ie){
					if(ie.Index==(float)joystick.Get("ongoing_drag")){
						return ;	
					}
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.x -= ie.Relative.y * V_LOOK_SENS;
					if(rot_deg.x < -90){ rot_deg.x=-90; }
					if(rot_deg.x > 90){ rot_deg.x=90; }
					rot_deg.y -= ie.Relative.x * H_LOOK_SENS;
					cam.RotationDegrees=rot_deg;
				}
			}
			else{
				if(@event is InputEventMouseMotion ie ){
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.x -= ie.Relative.y * V_LOOK_SENS;
					if(rot_deg.x < -90){ rot_deg.x=-90; }
					if(rot_deg.x > 90){ rot_deg.x=90; }
					rot_deg.y -= ie.Relative.x * H_LOOK_SENS;
					cam.RotationDegrees=rot_deg;
				}
			}
		}
		
	}
	
	public void playerDeath(){
		Translation=spawnpoint;
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
			if(Input.IsActionPressed("move_forward")){
				move_vec.z -= 1;
			}
			if(Input.IsActionPressed("move_backward")){
				move_vec.z += 1;
			}
			if(Input.IsActionPressed("move_left")){
				move_vec.x -= 1;
			}
			if(Input.IsActionPressed("move_right")){
				move_vec.x += 1;
			}
			move_vec=move_vec.Normalized();

			move_vec=move_vec.Rotated(new Vector3(0,1,0), cam.RotationDegrees.y*3.141592654F/180.0F);
			move_vec *= MOVE_SPEED;

			if(move_vec.Length() >= 0.1F){
				Vector3 rot_deg=cube.RotationDegrees;
				rot_deg.y=cam.RotationDegrees.y;
				cube.RotationDegrees=rot_deg;
				Vector3 rote_deg=cubeshape.RotationDegrees;
				rote_deg.y=cam.RotationDegrees.y;
				cubeshape.RotationDegrees=rote_deg;
			}

			just_jumped=false;
			bool grounded=IsOnFloor();
			y_velo -= GRAVITY;
			
			if(grounded && (Input.IsActionPressed("jump") || is_bt_jump_press) ){
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

			//Test Death
			if( Translation.y <= hauteur_min){
				playerDeath();
			}
		}
	}

	public void _on_TSB_jump_pressed(){
		is_bt_jump_press=true;
	}
	public void _on_TSB_jump_released(){
		is_bt_jump_press=false;
	}

	public void _on_TSB_menu_pressed(){
		EmitSignal("pause_bt_press");
	}

}
