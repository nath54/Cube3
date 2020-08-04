using Godot;
using System;

public class MenuPlayArcade : Control
{
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        //
        Label highscoreplats = (Label)GetNode("Bt_Plats/Label_highsocre");
        highscoreplats.Text="highscore : "+globale.highscore_plats;
    }

    public void _on_Bt_Plats_pressed(){
        
    }

    public void _on_Bt__pressed(){
        //GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
