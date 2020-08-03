using Godot;
using System;

public class Bt_Cube : TextureButton
{
    public int id;
    public Label labele;
    public string texte="";
    [Signal]
    public delegate void cliqued(int id);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        labele=(Label)GetNode("Label");
        labele.Text=texte;
        Connect("pressed", this, nameof(on_pressed));
    }

    public void on_pressed(){
        EmitSignal("cliqued", id);
    }


}
