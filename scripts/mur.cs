using Godot;
using System;

public class mur : Spatial
{
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
    }

    public void death(){
        globale.player_death();
    }

}
