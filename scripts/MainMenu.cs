using Godot;
using System;

public class MainMenu : Control
{
    private Button bt_start_arcade;
    private Button bt_start_adventure;
    private Button bt_settings;
    private Button bt_quit;
    private Control loading;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bt_start_arcade = (Button)GetNode("Bt_play_arcade");
        bt_start_adventure = (Button)GetNode("Bt_play_adventure");
        bt_settings = (Button)GetNode("Bt_settings");
        bt_quit = (Button)GetNode("Bt_quit");

        bt_start_arcade.Connect("pressed", this, nameof(onBtStartArcadePress));
        bt_start_adventure.Connect("pressed", this, nameof(onBtStartAdventurePress));
        bt_settings.Connect("pressed", this, nameof(onBtSettingsPress));
        bt_quit.Connect("pressed", this, nameof(onBtQuitPress));

        loading = (Control)GetNode("Loading");
        loading.Visible=false;

    }

    public void onBtStartArcadePress(){
        loading.Visible=true;
        GetTree().ChangeScene("res://menus/MenuPlayArcade.tscn");
    }
    public void onBtStartAdventurePress(){

    }

    public void onBtSettingsPress(){
        
    }
    
    public void onBtQuitPress(){
        GetTree().Quit();
        
    }

}

