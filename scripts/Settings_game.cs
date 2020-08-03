using Godot;
using System;

public class Settings_game : Control
{
    private Button bt_s_game;
    private Button bt_s_graphics;
    private Button bt_s_audio;
    private Button bt_s_other;

    public struct AncientSettings{    
    }
    
    public VScrollBar vscrollbare;
    public VBoxContainer vboxcontainere;
    public Global globale;
    public PopupDialog popup;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale=(Global)GetNode("/root/Global");
        //
        bt_s_game = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_game");
        bt_s_graphics = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_graphics");
        bt_s_audio = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_audio");
        bt_s_other = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_other");
        //
        popup = (PopupDialog)GetNode("PopupDialog");
        vscrollbare = (VScrollBar)GetNode("VScrollBar");
        vboxcontainere = (VBoxContainer)GetNode("Settings/VBoxContainer");
        //Initialisation des Layout settings
        
    }

    public void _on_Bt_game_pressed(){ GetTree().ChangeScene("res://menus/Settings_game.tscn"); }
    public void _on_Bt_graphics_pressed(){ GetTree().ChangeScene("res://menus/Settings_graphics.tscn"); }
    public void _on_Bt_audio_pressed(){ GetTree().ChangeScene("res://menus/Settings_audio.tscn"); }
    public void _on_Bt_other_pressed(){ GetTree().ChangeScene("res://menus/Settings_other.tscn"); }

    public void on_apply(){
        
        //Save Settings
        ProjectSettings.Save();
        globale.SaveGame();
        //
        popup.Popup_();
    }


    public void on_cancel(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void keep_settings(){
        popup.Hide();
        GetTree().Quit();        
    }

    public void on_quit(){
        popup.Hide();
        GetTree().ChangeScene("res://menus/Settings_game.tscn");
    }

    public void _on_VScrollBar_value_changed(float value){
        vboxcontainere.RectPosition=new Vector2(0, -value*10);
    }

}
