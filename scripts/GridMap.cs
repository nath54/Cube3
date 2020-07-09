using Godot;
using System;

public class GridMap : Godot.GridMap
{
    OpenSimplexNoise noise = new OpenSimplexNoise();

    public void generatePlatforms(){

    }

    public void generateMaze(){

    }

    public void generate(string tipe){
        if(tipe=="platforms"){
            generatePlatforms();
        }
        else if(tipe=="maze"){
            generateMaze();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
    }

}
