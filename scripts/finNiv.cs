using Godot;
using System;

public class finNiv : Spatial
{
    public AnimationPlayer an1;
    public AnimationPlayer an2;
    public AnimationPlayer an3;
    [Signal]
    public delegate void bodyTouched(Node body);
    [Signal]
    public delegate void animation_finished(string animation);
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        globale = (Global)GetNode("/root/Global");
        globale.finnive=this;
        /*
        an1.CurrentAnimation="Bras_Sphere1Action";
        an2.CurrentAnimation="Bras_Sphere2Action";
        an3.CurrentAnimation="Bras_Sphere3Action";
        an1.Play();
        an2.Play();
        an3.Play();
        */              
    }

    public override void _Process(float delta){
        if(!an1.IsPlaying()){ an1.Play(); }
        if(!an2.IsPlaying()){ an2.Play(); }
        if(!an3.IsPlaying()){ an3.Play(); }
    }

    public override void _EnterTree(){
        an1=(AnimationPlayer)GetNode("Bras_Sphere1/AnimationPlayer");
        an2=(AnimationPlayer)GetNode("Bras_Sphere2/AnimationPlayer");
        an3=(AnimationPlayer)GetNode("Bras_Sphere3/AnimationPlayer");
        an1.Autoplay="Bras_Sphere1Action";
        an2.Autoplay="Bras_Sphere2Action";
        an3.Autoplay="Bras_Sphere3Action";
    }

    public void an1stop(string an){
        an1.Play();
    }

    public void an2stop(string an){
        an2.Play();
    }
    public void an3stop(string an){
        an3.Play();
    }

    public void _on_Area_body_entered(Node body){
        EmitSignal("bodyTouched", body);
    }

}
