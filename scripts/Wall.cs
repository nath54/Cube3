using Godot;
using System;

public class Wall : MeshInstance
{
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _on_Area_body_entered(Node body){
        GD.Print(body);
    }

    public void _on_Area_body_shape_entered(int body_id, Node body, int body_shape, int area_shape){
		GD.Print("id : ",body_id," node : ",body);
	}
}
