using Godot;
using System;

public class Pause_Menu : Control
{
    private Button bt_resume;
    private Button bt_quit_to_menu;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bt_resume = (Button)GetNode("Bt_resume");
        bt_quit_to_menu = (Button)GetNode("Bt_quit_to_menu");

        bt_resume.Connect("pressed", this, nameof(onBtResumePress));
        bt_quit_to_menu.Connect("pressed", this, nameof(onBtQuitToMenuPress));
    }

    public void onBtResumePress(){
        EmitSignal("resume");
    }

    public void onBtQuitToMenuPress(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }
}
