using Godot;
using System;

public class Walle : MeshInstance
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Mur ready");
    }

    public void _on_Area_body_entered(Node body){
        GD.Print("mur collision : ",body);
    }
}
