using Godot;
using System;

public class Joystick : Sprite
{
    private TouchScreenButton tcbt;
    [Signal]
    public delegate void get_value(Vector2 value);
    [Signal]
    public delegate void begin_press(bool pressed);
    [Signal]
    public delegate void end_press(bool pressed);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tcbt = (TouchScreenButton)GetNode("TouchScreenButton");
        tcbt.Connect("get_value", this, nameof(onGetValueSignal));
        tcbt.Connect("begin_press", this, nameof(onBeginPress));
        tcbt.Connect("end_press", this, nameof(onEndPress));
    }

    public void onBeginPress(){
        EmitSignal("begin_press");
    }

    public void onEndPress(){
        EmitSignal("end_press");
    }

    public void onGetValueSignal(Vector2 value){
        //GD.Print("Joystick value getted : ",value);
        EmitSignal("get_value", value);
    }


}
