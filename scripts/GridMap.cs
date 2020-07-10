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
    public int nb_plats=20;
    public int depx=1;
    public int depz=1;
    public int depy=1;
    public int finx=1;
    public int finy=1;
    public int finz=1;
    public Color floor_color=new Color(100,100,100);
    public Color wall_color=new Color(100,100,100);
    public void generatePlatforms(){
        GD.Print("platform");
        Random rand = new Random();
        depx=rand.Next(1,tx-1);
        depz=rand.Next(1,tz-1);
        depy=rand.Next(1,ty-1);
        int security=0;
        int max_security=50;
        SetCellItem(depx,depy,depz,0);
        int ax=depx;
        int ay=depy;
        int az=depz;
        for(int w=0; w<=nb_plats; w++){
            if(security<max_security){
                security=0;
                int dx=0;
                int dy=0;
                int dz=0;
                bool derange=false;
                do{
                    derange=false;
                    int g=rand.Next(0,3);
                    if(g==0){
                        dx=Convert.ToInt32(rand.Next(3,7));
                        if(rand.Next(0,2)==0){ dx*=-1; }
                    }
                    else if(g==1){
                        dz=Convert.ToInt32(rand.Next(3,7));
                        if(rand.Next(0,2)==0){ dz*=-1; }
                    }
                    dy=Convert.ToInt32(rand.Next(1,3));
                    if(rand.Next(0,2)==0){ dy*=-1; }
                    for(int xx=-2; xx<2; xx++){
                        for(int zz=-2; zz<2; zz++){
                            for(int yy=2; yy>=-2; yy--){
                                if(GetCellItem(ax+dx+xx,ay+dy+yy,az+dz+zz)>=0){
                                    derange=true;
                                    GD.Print("dérange");
                                }
                            }
                        }       
                    }
                    security+=1;
                }while(derange && security<max_security);
                if(!derange){
                    ax+=dx;
                    ay+=dy;
                    az+=dz;
                    if(ax<=0){ ax=1; }
                    if(az<=0){ az=1; }
                    if(ay<=0){ ay=1; }
                    if(ax>=tx){ ax=tx-1; }
                    if(az>=tz){ az=tz-1; }
                    if(ay>=ty){ ay=ty-1; }
                    SetCellItem(ax,ay,az,0);
                }
            }
        }
        finx=ax;
        finy=ay;
        finz=az;
    }

    public void generateMaze2D(){
        //modification des materiaux
        MeshLibrary ml = MeshLibrary;
        
        //creation du sol
        for(int x=0; x<=tx; x++){
            for(int z=0; z<=tx; z++){
                SetCellItem(x,0,z,0);
            }
        }
        //creation des murs
        for(int x=0; x<=tx; x++){
            for(int z=0; z<=tx; z++){
                SetCellItem(x,1,z,1);
            }
        }
        //on remove les murs
        Random rand = new Random();
        depx=rand.Next(1,tx-1);
        depz=rand.Next(1,tz-1);
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
