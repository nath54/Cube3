using Godot;
using System;

public class ButtonSkinSelector : Button
{
    [Signal]
    public delegate void clique(int id);
    [Export]
    public int id=0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connect("pressed", this, nameof(_on_ButtonSkinSelector_pressed));
    }

    public void _on_ButtonSkinSelector_pressed(){
        //GD.Print("c "+id);
        EmitSignal("clique", id);
    }

}
