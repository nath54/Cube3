using Godot;
using System;

public class Bt_Rect : TextureButton
{
    public int id;
    public string texte;

    [Signal]
    public delegate void clique(int id);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Label text = (Label)GetNode("Label");
        text.Text=texte;
        Connect("pressed", this, nameof(cliquer));
    }

    public void cliquer(){
        EmitSignal("clique", id);
    }
 
}
