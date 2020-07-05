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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
