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
	public Spatial cube;
	public Spatial cam;
	public Joystick_Button joystick;
	public Sprite global_joystick;
	public TouchScreenButton bt_menu;
	public TouchScreenButton bt_jump;
	public TouchScreenButton bt_respawn;
	public TouchScreenButton bt_cam;
	public Vector3 spawnpoint;
	public CollisionShape cubeshape;
	public Label debug;
	public bool just_jumped;
	public bool paused = false;
	public float taille = 1;
	public Area areacol;
	public Global globale;
	public Camera camerae;
	public Spatial flecheBase;
	public float joysticke_x=0;
	public float joysticke_y=0;
	public int current_camera=0; //0=normal camera ; 1=topdown camera
	public string[] cams = {"CamBase/Camera1", "Camera2"};
	[Signal]
	public delegate void onPauseBtPress();
	public const float rayLength = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globale = (Global)GetNode("/root/Global");
		//
		globale.player=this;

		//
		cam = (Spatial)GetNode("CamBase");
		camerae = (Camera)GetNode(cams[current_camera]);
		cube = (Spatial)GetNode("cube");
		cubeshape = (CollisionShape)GetNode("CollisionShape");
		joystick = (Joystick_Button)GetNode("joystick/Joystick_Button");
		global_joystick = (Sprite)GetNode("joystick");
		bt_jump = (TouchScreenButton)GetNode("TSB_jump");
		bt_menu = (TouchScreenButton)GetNode("TSB_menu");
		bt_respawn = (TouchScreenButton)GetNode("TSB_respawn");
		bt_cam = (TouchScreenButton)GetNode("TSB_cam");
		flecheBase = (Spatial)GetNode("FlechBase");
		debug = (Label)GetNode("debug");

		if(globale.difficulty>1){
			flecheBase.Visible=false;
		}

		if( globale.is_mobile() ){
		}
		else{
			global_joystick.Visible=false;
			global_joystick.SetProcessUnhandledInput(false);
			bt_jump.Visible=false;
			bt_jump.SetProcessUnhandledInput(false);
			bt_menu.Visible=false;
			bt_menu.SetProcessUnhandledInput(false);
			bt_respawn.Visible=false;
			bt_respawn.SetProcessUnhandledInput(false);
			bt_cam.Visible=false;
			bt_cam.SetProcessUnhandledInput(false);
		}
		if(!globale.respawn){
			bt_respawn.Visible=false;
			bt_respawn.SetProcessUnhandledInput(false);
		}
		//
		spawnpoint=Translation;
		spawnpoint.y-=0.05F;
		//
		just_jumped=false;
		//
		areacol = (Area)GetNode("Area");
		//
		setSkin(globale.skin_id_equipe);
		//
	}

	public void respawn(){
		if(globale.respawn){
			Translation=spawnpoint;
		}
	}

	public void setSkin(int idskin){
		PackedScene s;
		if(idskin<globale.max_skin){
			s = (PackedScene)GD.Load("res://player/skins/skin_"+idskin+".tscn");
		}
		else{
			s = (PackedScene)GD.Load("res://player/skins/skin_0.tscn");
		}
		Spatial skin = (Spatial)s.Instance();
		cube.AddChild(skin);
	}

	public void change_cam(){
		current_camera+=1;
		if(current_camera>1){ current_camera=0; }
		camerae.Current = false;
		camerae = (Camera)GetNode(cams[current_camera]);
		camerae.Current = true;
		camerae.Environment=globale.camenv;
	}

	public override void _Input(InputEvent @event)
	{
		if(!paused){
			if(Input.IsActionJustPressed("respawn")){
				respawn();
			}
			if(current_camera==0){
				if(globale.is_mobile()){
					if(@event is InputEventScreenDrag ie){
						if(ie.Index==joystick.ongoing_drag){
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

		if(Input.IsActionJustPressed("change_cam")){
			change_cam();
		}
		
	}
	
	public void playerDeath(){
		if(globale.respawn){
			respawn();
		}
		else{
			globale.levele.partiePerdue();
		}
	}

	public void updateflechedir(){
		if(globale.difficulty<=1){
			flecheBase.LookAt(globale.finnive.GlobalTransform.origin, Vector3.Up);
		}
		else if(flecheBase.Visible){
			flecheBase.Visible=false;
		}
	}

	public override void _Process(float delta){
		updateflechedir();
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
			if(globale.is_mobile()){
				Vector2 joymov=joystick.get_value();
				move_vec.x=joymov.x;
				move_vec.z=joymov.y;
			}
			else{
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
				if(Input.IsActionPressed("rot_cam_right")){
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.y -= 10 * H_LOOK_SENS;
					cam.RotationDegrees=rot_deg;
				}
				if(Input.IsActionPressed("rot_cam_left")){
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.y += 10 * H_LOOK_SENS;
					cam.RotationDegrees=rot_deg;
				}
				if(Input.IsActionPressed("rot_cam_up")){
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.x -= 10 * V_LOOK_SENS;
					if(rot_deg.x < -90){ rot_deg.x=-90; }
					cam.RotationDegrees=rot_deg;
				}
				if(Input.IsActionPressed("rot_cam_down")){
					Vector3 rot_deg=cam.RotationDegrees;
					rot_deg.x += 10 * V_LOOK_SENS;
					if(rot_deg.x > 90){ rot_deg.x=90; }
					cam.RotationDegrees=rot_deg;
				}
			}			
			move_vec=move_vec.Normalized();
			if(current_camera==0){
				move_vec=move_vec.Rotated(new Vector3(0,1,0), cam.RotationDegrees.y*3.141592654F/180.0F);
			}
			move_vec *= MOVE_SPEED;
			if(move_vec.Length() >= 0.1F){
				Vector3 rot_deg=cube.RotationDegrees;
				if(current_camera==0){
					rot_deg.y=cam.RotationDegrees.y;
				}				
				else{
					float r = 0;
					rot_deg.y=r;
				}
				cube.RotationDegrees=rot_deg;
				Vector3 rote_deg=cubeshape.RotationDegrees;
				rote_deg.y=cam.RotationDegrees.y;
				cubeshape.RotationDegrees=rote_deg;
			}
			just_jumped=false;
			bool grounded=IsOnFloor();
			y_velo -= GRAVITY;			
			if(grounded && (Input.IsActionPressed("jump")) ){
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
			//camera
			/*
			Vector2 pos = new Vector2((float)ProjectSettings.GetSetting("display/window/size/width")/2,(float)ProjectSettings.GetSetting("display/window/size/height")/2);
			var from = camerae.ProjectRayOrigin(pos);
	        var to = from + camerae.ProjectRayNormal(pos) * rayLength;
			var spaceState = GetWorld().DirectSpaceState;
        	var result = spaceState.IntersectRay(camerae.Translation, camerae.Translation, new object[] { this });
			*/
		}
	}

	public void _on_TSB_menu_pressed(){
		EmitSignal("onPauseBtPress");
		//debug.Text="menu";
	}

	public void _on_TSB_cam_pressed(){
		change_cam();
	}

}
