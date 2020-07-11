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

        loading = (Control)GetNode("Loading");
        loading.Visible=false;

    }

    public void _on_Bt_play_arcade_pressed(){
        loading.Visible=true;
        GetTree().ChangeScene("res://menus/MenuPlayArcade.tscn");
    }
    public void _on_Bt_play_adventure_pressed(){

    }

    public void _on_Bt_settings_pressed(){
        
    }
    
    public void _on_Bt_quit_pressed(){
        GetTree().Quit();
    }

    public void _on_Bt_Skins_pressed(){
        GetTree().ChangeScene("res://menus/MenuSkins.tscn");
    }

}

