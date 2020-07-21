using Godot;
using System;

public class finNiv : Spatial
{
    public AnimationPlayer an1;
    public AnimationPlayer an2;
    public AnimationPlayer an3;
    [Signal]
    public delegate void bodyTouched(Node body);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        an1=(AnimationPlayer)GetNode("Bras_Sphere1/AnimationPlayer");
        an2=(AnimationPlayer)GetNode("Bras_Sphere2/AnimationPlayer");
        an3=(AnimationPlayer)GetNode("Bras_Sphere3/AnimationPlayer");
        an1.CurrentAnimation="Bras_Sphere1Action";
        an2.CurrentAnimation="Bras_Sphere2Action";
        an3.CurrentAnimation="Bras_Sphere3Action";
        an1.Play();
        an2.Play();
        an3.Play();
        
        
    }

    public void an1stop(){
        an1.Play();
    }

    public void an2stop(){
        an2.Play();
    }
    public void an3stop(){
        an3.Play();
    }

    public void _on_Area_body_entered(Node body){
        EmitSignal("bodyTouched", body);
    }

}
