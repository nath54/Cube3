using Godot;
using System;

public class finNiv : Spatial
{
    public Area area_fin;

    [Signal]
    public delegate void bodyTouched(Node body);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _on_Area_body_entered(Node body){
        EmitSignal("bodyTouched", body);
    }

}
