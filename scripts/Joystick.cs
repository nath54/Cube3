using Godot;
using System;

public class Joystick : Sprite
{
    private TouchScreenButton tcbt;

    [Signal]
    public delegate void get_value(Vector2 value);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tcbt = (TouchScreenButton)GetNode("TouchScreenButton");
        tcbt.Connect("get_value", this, nameof(onGetValueSignal));
    }

    public void onGetValueSignal(Vector2 value){
        //GD.Print("Joystick value getted : ",value);
        EmitSignal("get_value", value);
    }


}
