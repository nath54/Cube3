using Godot;
using System;

public class Settings_graphics : Control
{
    private Button bt_s_game;
    private Button bt_s_graphics;
    private Button bt_s_audio;
    private Button bt_s_other;
    private TextEdit te_width;
    private TextEdit te_height;
    private CheckBox cb_showfps;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bt_s_game = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_game");
        bt_s_graphics = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_graphics");
        bt_s_audio = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_audio");
        bt_s_other = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_other");
        cb_showfps = (CheckBox)GetNode("Settings/VBoxContainer/St_showfps/Cb_showfps");
        te_width = (TextEdit)GetNode("Settings/VBoxContainer/St_dwidth/te_width");
        te_height = (TextEdit)GetNode("Settings/VBoxContainer/St_dheight/te_height");
    }

    public void _on_Bt_game_pressed(){ GetTree().ChangeScene("res://menus/Settings_game.tscn"); }
    public void _on_Bt_graphics_pressed(){ GetTree().ChangeScene("res://menus/Settings_graphics.tscn"); }
    public void _on_Bt_audio_pressed(){ GetTree().ChangeScene("res://menus/Settings_audio.tscn"); }
    public void _on_Bt_other_pressed(){ GetTree().ChangeScene("res://menus/Settings_other.tscn"); }

    public void _on_te_height_text_changed(){
        string val=te_height.Text;
        if(val.Length>=1){
            double value;
            if (double.TryParse(val, out value)){
                double nvalue=value*(16.0/9.0);
                string nval=""+nvalue;
                te_width.Text=nval;
            }
        }
    }

    public void _on_te_width_text_changed(){
        string val=te_width.Text;
        if(val.Length>=1){
            double value;
            if (double.TryParse(val, out value)){
                double nvalue=value/(16.0/9.0);
                string nval=""+nvalue;
                te_height.Text=nval;
            }
        }
    }
}
