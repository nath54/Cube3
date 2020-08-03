using Godot;
using System;

public class videoPlayer : Control
{
    public VideoPlayer vid;
    public Global globale;
    public bool paused=false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        vid = (VideoPlayer)GetNode("VideoPlayer");
        //
        if(globale.vidpath==""){
            on_finished();
        }
        else{
            vid.Stream=ResourceLoader.Load(globale.vidpath) as VideoStreamWebm;
            vid.Play();
        }
    }

    public void on_click(){
        if(paused){
            paused=false;
            vid.Paused=paused;
        }
        else{
            paused=true;
            vid.Paused=paused;
        }
    }

    public void _on_VideoPlayer_gui_input(InputEvent @event){
         if(@event is InputEventScreenTouch evente){
             on_click();
         }
    }

    public void on_finished(){
        if(globale.vidscenepath==""){
            GetTree().ChangeScene("res://menus/MainMenu.tscn");
        }
        else{
            GetTree().ChangeScene(globale.vidscenepath);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
