using Godot;
using System;

public class World : Spatial
{
    
    private Control pause_menu;
    private Player player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        //Input.SetMouseMode(Input.MouseMode.Captured);
        //
        player= (Player)GetNode("Player");
    }

    public void onPauseMenuBtResumePress(){
        GD.Print("resume !");
        Resume();
    }

    public void Resume(){
        pause_menu.Visible=false;
        Input.SetMouseMode(Input.MouseMode.Captured);
        player.paused=false;
    }

    public override void _Input(InputEvent @event){
        if(Input.IsActionJustPressed("menu")){
            if(pause_menu.Visible){
                Resume();
            }
            else{
                pause_menu.Visible=true;
                Input.SetMouseMode(Input.MouseMode.Visible);
                player.paused=true;
            }
        }
    }

   

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}