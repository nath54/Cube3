using Godot;
using System;

public class ButtonSkinSelector : Button
{
    [Signal]
    public delegate void clique(int id);
    public int id=0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _on_ButtonSkinSelector_pressed(){
        EmitSignal("clique", id);
    }

}
