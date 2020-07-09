using Godot;
using System;

public class MenuPlayArcade : Control
{
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
    }

    public void _on_Bt_Maze1_pressed(){
        globale.mtx=32;
        globale.mtz=32;
        globale.nbchem=32;
        globale.timemax=180;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Maze2_pressed(){
        globale.mtx=100;
        globale.mtz=100;
        globale.nbchem=100;
        globale.timemax=250;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Maze3_pressed(){
        globale.mtx=150;
        globale.mtz=150;
        globale.nbchem=150;
        globale.timemax=400;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
