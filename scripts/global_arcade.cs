using Godot;
using System;

public class global_arcade : Node
{
    
    public int actual_level;
    public int actual_difficulty; // 0=baby , 1=easy , 2=normal , 3=hard , 4=hardcore
    // Called when the node enters the scene tree for the first time.
    public Vector3 player_spawn;
    public int nb_platforms;
    public Vector3 Grid_Size;

    public override void _Ready()
    {
        actual_level=1;
        actual_difficulty=2;
        nb_platforms=20;
        Grid_Size=new Vector3(100,20,100);

    }
}
