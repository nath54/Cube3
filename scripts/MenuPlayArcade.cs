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

    public void _on_Bt_Maze_pressed(){
        globale.mtx=32;
        globale.mtz=32;
        globale.nbchem=32;
        globale.timemax=180;
        globale.tipe="maze";
        globale.player_taille=0.3F;
        globale.grid_scale=new Vector3(2,2,2);
        globale.grid_cell_scale=1;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Plats_pressed(){
        globale.mtx=40+5*globale.level;
        globale.mtz=40+5*globale.level;
        globale.nb_plats=20+globale.level;
        globale.timemax=180+5*globale.level;
        globale.tipe="platforms";
        globale.player_taille=0.5F;
        globale.grid_scale=new Vector3(1.2F,1.2F,1.2F);
        globale.grid_cell_scale=1;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt__pressed(){
        //GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
