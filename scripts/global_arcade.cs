using Godot;
using System;

public class global_arcade : Node
{
    public int actual_level=1;
    public int actual_difficulty=2; // 0=baby , 1=easy , 2=normal , 3=hard , 4=hardcore
    // Called when the node enters the scene tree for the first time.
    public Vector3 player_spawn;

    public int nb_platforms = 20;

    public Vector3 Grid_Size = new Vector3(100,20,100);

}
