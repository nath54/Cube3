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
    public int finx=1;
    public int finy=1;
    public int finz=1;
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
        depy=2;
        SetCellItem(depx,1,depz, -1);
        int ax=depx;
        int az=depz;
        for(int w=0; w<nbchem; w++){
            if(rand.Next(1,3)==1){
                if(rand.Next(1,3)==1){ ax+=1; }
                else{ ax-=1; }
            }
            else{
                if(rand.Next(1,3)==1){ az+=1; }
                else{ az-=1; }
            }
            if(ax<=0){ ax=1; }
            if(az<=0){ az=1; }
            if(ax>=tx){ ax=tx-1; }
            if(az>=tx){ az=tz-1; }
            SetCellItem(ax,1,az, -1);
        }
        finx=ax;
        finy=1;
        finz=az;
        
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
