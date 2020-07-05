using Godot;
using System;

public class World : Spatial
{
    
    private Control pause_menu;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _Input(InputEvent @event){
        if(Input.IsActionJustPressed("menu")){
            if(pause_menu.Visible){
                pause_menu.Visible=false;
                Input.SetMouseMode(Input.MouseMode.Captured);
            }
            else{
                pause_menu.Visible=true;
                Input.SetMouseMode(Input.MouseMode.Visible);
            }
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
