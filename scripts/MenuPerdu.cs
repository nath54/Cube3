using Godot;
using System;

public class MenuPerdu : Control
{

    public bool is_mobile(){
		return (OS.GetName()=="Android" || OS.GetName()=="iOS");
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if(!is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }   
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void _on_Bt_Quit_pressed(){
        GetTree().Quit();
    }

}
