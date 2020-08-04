using Godot;
using System;

public class PreArcadeLevel : Control
{
    Global globale;
    AnimationPlayer animationPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale = (Global)GetNode("/root/Global");
        //
        animationPlayer=(AnimationPlayer)GetNode("L_click/AnimationPlayer");
        animationPlayer.CurrentAnimation="clignotement";
        //
        string[] tps={"platforms"};
        Random rand = new Random();
        globale.tipe=tps[rand.Next(0,tps.Length)];
        if(globale.tipe=="platforms"){ prepare_platform(); }
        //
        Label l_dif = (Label)GetNode("L_difficulty");
        Label l_time = (Label)GetNode("L_time");
        Label l_titre = (Label)GetNode("Titre");
        Label l_click = (Label)GetNode("L_click");
        //
        l_dif.Text = "Difficulty : "+globale.difs[globale.difficulty];
        l_titre.Text = "level "+globale.level+" : "+globale.tipe;
        l_time.Text = "time maximum : "+globale.timemax+" sec";
        //
        l_click.Text="click or press "+((InputEvent)InputMap.GetActionList("jump")[0]).AsText()+" to play ...";
    }

    public void prepare_platform(){
        globale.mtx=80+5*globale.level;
        globale.mtz=80+5*globale.level;
        globale.mty=80+5*globale.level;
        globale.nb_plats=10+globale.level*2;
        globale.tipe="platforms";
        //
        globale.timemax=globale.nb_plats*6;
        //
        if(globale.difficulty==0){ globale.timemax*=1.5F; }
        else if(globale.difficulty==2){ globale.timemax/=1.75F; }
        else if(globale.difficulty==3){ globale.timemax/=2.5F; }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {               
        if(!animationPlayer.IsPlaying()){
            animationPlayer.Play();
        }            
    }

    public override void _Input(InputEvent @event){
        if(@event is InputEventScreenTouch){
            GetTree().ChangeScene("res://levels/World.tscn");
        }
        else if(@event is InputEventKey ie){
            if(ie.IsActionPressed("jump")){
                GetTree().ChangeScene("res://levels/World.tscn");
            }
        }
    }

}
