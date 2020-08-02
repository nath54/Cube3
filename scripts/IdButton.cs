using Godot;
using System;

public class IdButton : Button
{
    public int id;
    [Signal]
    public delegate void cliqued(int id);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("pressed", this, nameof(on_pressed));
    }

    public void on_pressed(){
        EmitSignal("cliqued", id);
    }

}
