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

    public string[] difs = {"easy","normal","hard","hell"};
    public string[] difs_explains={
        @"
    -Time * 1.5
    -Larger Platforms (Arcade)
    -Rewards / 1.5 (Arcade)",
        @"",
        @"
    -Time/1.75
    -Smaller platforms (Arcade)
    -Rewards * 1.5 (Arcade)",
        @"
    -No Respawn
    -Time/2.5
    -Really smaller platforms (Arcade)
    -But no fake-platforms
    (because we die directly)
    -Rewards * 3 (Arcade)",
    };

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
        //difficulty
        HScrollBar hsb_dif = (HScrollBar)GetNode("Settings/VBoxContainer/St_difficulty/HScrollBar");
        hsb_dif.MaxValue=difs.Length;
        hsb_dif.Page=1;
        hsb_dif.Value=globale.difficulty;
        on_hsb_dif_changed((float)hsb_dif.Value);
        //
    }

    public void _on_Bt_game_pressed(){ GetTree().ChangeScene("res://menus/Settings_game.tscn"); }
    public void _on_Bt_graphics_pressed(){ GetTree().ChangeScene("res://menus/Settings_graphics.tscn"); }
    public void _on_Bt_audio_pressed(){ GetTree().ChangeScene("res://menus/Settings_audio.tscn"); }
    public void _on_Bt_other_pressed(){ GetTree().ChangeScene("res://menus/Settings_other.tscn"); }

    public void on_apply(){
        //DIFFICULTY
        HScrollBar hsb_dif = (HScrollBar)GetNode("Settings/VBoxContainer/St_difficulty/HScrollBar");
        globale.difficulty=(int)hsb_dif.Value;
        //Save Settings
        ProjectSettings.Save();
        globale.SaveGame();
        //
        //popup.Popup_();
    }

    public void on_hsb_dif_changed(float value){
        Label titre=(Label)GetNode("Settings/VBoxContainer/St_difficulty/Label");
        Label explain=(Label)GetNode("Settings/VBoxContainer/St_difficulty/L_explain");
        //
        titre.Text="difficulty : "+difs[(int)value];
        explain.Text=difs_explains[(int)value];
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
