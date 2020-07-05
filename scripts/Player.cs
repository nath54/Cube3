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
			cam.rotation_degrees.y -= Event.relative.x * H_LOOK_SENS; 
		}
	}

	public override void _PhysicProcess(float delta)
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
