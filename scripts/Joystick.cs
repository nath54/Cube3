using Godot;
using System;

public class Joystick : Sprite
{
    private TouchScreenButton tcbt;
    [Signal]
    public delegate void get_value(Vector2 value);
    [Signal]
    public delegate void begin_press();
    [Signal]
    public delegate void end_press();
    [Signal]
    public delegate void camera_begin_press();
    [Signal]
    public delegate void camera_end_press();

    [Signal]
    public delegate void pause_bt_press();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tcbt = (TouchScreenButton)GetNode("TouchScreenButton");
        tcbt.Connect("get_value", this, nameof(onGetValueSignal));
        tcbt.Connect("begin_press", this, nameof(onBeginPress));
        tcbt.Connect("end_press", this, nameof(onEndPress));
        tcbt.Connect("camera_begin_press", this, nameof(onCameraBeginPress));
        tcbt.Connect("camera_end_press", this, nameof(onCameraEndPress));
    }

    public void onCameraBeginPress(){ EmitSignal("camera_begin_press"); }
    public void onCameraEndPress(){ EmitSignal("camera_end_press"); }
    public void onBeginPress(){ EmitSignal("begin_press"); }
    public void onEndPress(){ EmitSignal("end_press"); }

    public void onGetValueSignal(Vector2 value){
        //GD.Print("Joystick value getted : ",value);
        EmitSignal("get_value", value);
    }

    public void _on_Bt_pause_pressed(){
        EmitSignal("pause_bt_press");
    }

}
