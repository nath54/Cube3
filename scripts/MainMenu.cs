using Godot;
using System;

public class MainMenu : Control
{
    private TextureButton bt_start_arcade;
    private TextureButton bt_start_adventure;
    private TextureButton bt_settings;
    private TextureButton bt_quit;
    private Control loading;
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale = (Global)GetNode("/root/Global");
        //
        bt_start_arcade = (TextureButton)GetNode("Bt_play_arcade");
        bt_start_adventure = (TextureButton)GetNode("Bt_play_adventure");
        bt_settings = (TextureButton)GetNode("Bt_settings");
        bt_quit = (TextureButton)GetNode("Bt_quit");

        loading = (Control)GetNode("Loading");
        loading.Visible=false;

    }

    public void _on_Bt_play_arcade_pressed(){
        loading.Visible=true;
        globale.level=1;
        GetTree().ChangeScene("res://menus/PreArcadeLevel.tscn");
    }
    public void _on_Bt_play_adventure_pressed(){
        GetTree().ChangeScene("res://menus/MenuLCats.tscn");
    }

    public void _on_Bt_settings_pressed(){
        GetTree().ChangeScene("res://menus/Settings_game.tscn");
    }
    
    public void _on_Bt_quit_pressed(){
        GetTree().Quit();
    }

    public void _on_Bt_Skins_pressed(){
        GetTree().ChangeScene("res://menus/MenuSkins.tscn");
    }

}

