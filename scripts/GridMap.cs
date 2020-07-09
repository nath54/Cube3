using Godot;
using System;

public class GridMap : Godot.GridMap
{
    OpenSimplexNoise noise = new OpenSimplexNoise();

    public string tipe="platforms";
    public int tx = 32;
    public int tz = 32;
    public int ty = 32;
    public int nbchem=15;
    public int depx=1;
    public int depz=1;
    public int depy=1;
    public void generatePlatforms(){

    }

    public void generateMaze2D(){
        //creation du sol
        for(int x=0; x<tx; x++){
            for(int z=0; z<tx; z++){
                SetCellItem(x,0,z,0);
            }
        }
        //creation des murs
        for(int x=0; x<tx; x++){
            for(int z=0; z<tx; z++){
                SetCellItem(x,1,z,1);
            }
        }
        //on remove les murs
        Random rand = new Random();
        depx=rand.Next(1,tx-1);
        depz=rand.Next(1,ty-1);
        int ax=depx;
        int az=depz;
        for(int w=0; w<nbchem; w++){
            SetCellItem(ax,1,az, -1);
            ax+=rand.Next(-1,2);
            ay+=rand.Next(-1,2);
        }
    }

    public void generate(){
        if(tipe=="platforms"){
            generatePlatforms();
        }
        else if(tipe=="maze"){
            generateMaze2D();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
    }

}
