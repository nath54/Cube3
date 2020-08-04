using System.Linq;
using Godot;
using System;

public class GenerateArcade : Node
{
    Global globale;

    public override void _Ready(){
        globale=(Global)GetNode("/root/Global");
    }

    public bool test_derange(Vector3 pos,int[,,] mape, int mtx, int mty, int mtz){
        int[] derange={1};
        for(int xx=-5; xx<5; xx++){
            for(int yy=-10; yy<10; yy++){
                for(int zz=-5; zz<5; zz++){
                    if(pos.x+xx>=0 && pos.z+zz>=0 && pos.y+yy>=0 && pos.x+xx<mtx && pos.z+zz<mtz && pos.y+yy<mty){
                        if(derange.Contains(mape[(int)pos.x+xx,(int)pos.y+yy,(int)pos.z+zz])){
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public (int,int,int) decalage_platforms(Vector3 tc){
        Random rand = new Random();
        int dx=0,
            dy=0,
            dz=0;
        int minxz=2;
        int maxxz=10;
        int miny=2;
        int maxy=8;
        double jump_distance=0;
        double max_jump_distance=10;
        if(globale.difficulty==0){
            minxz=2;
            maxxz=5;
            maxy=5;
            max_jump_distance=10;
        }
        else if(globale.difficulty==1){
            minxz=2;
            maxxz=7;
            maxy=5;
            max_jump_distance=9;
        }
        else if(globale.difficulty==2){
            minxz=2;
            maxxz=8;
            maxy=5;
            max_jump_distance=8;
        }
        else if(globale.difficulty==3){
            minxz=2;
            maxxz=6;
            maxy=4;
            max_jump_distance=5;
        }
        do{
            //DX
            if(rand.Next(0,2)==1){ dx=rand.Next(minxz,maxxz); }
            else if(rand.Next(0,2)==1){ dx=rand.Next(-maxxz,-minxz); }
            //DZ
            if(rand.Next(0,2)==1){ dz=rand.Next(minxz,maxxz); }
            else if(rand.Next(0,2)==1){ dz=rand.Next(-maxxz,-minxz); }
            //DY
            if(rand.Next(0,2)==1){ dy=rand.Next(miny,maxy); }
            else if(rand.Next(0,2)==1){ dy=rand.Next(-8,-1); }        
            //JUMP DISTANCE
            jump_distance=Math.Sqrt( Math.Pow(dx,2) + Math.Pow(dy,3) + Math.Pow(dz,2) );
        } while(jump_distance<max_jump_distance);
        
        return (dx,dy,dz);
    }

    public (Spatial,Vector3,Vector3,int) generate_platforms_1(){
        // 0 = vide , 1=floor , 2=fake floor , 3=wall
        Vector3 dc = new Vector3(1F,1F,1F);
        Vector3 tc = new Vector3(1F,1F,1F);
        if(globale.difficulty==0){
            dc = new Vector3(2.5F,1F,2.5F);
            tc = new Vector3(2.5F,1F,2.5F);
        }
        else if(globale.difficulty==1){
            dc = new Vector3(1.5F,1F,1.5F);
            tc = new Vector3(1.5F,1F,1.5F);
        }
        else if(globale.difficulty==3){
            dc = new Vector3(1.5F,1.5F,1.5F);
            tc = new Vector3(0.8F,0.8F,0.8F);
        }
        int mtx=globale.mtx;
        int mty=globale.mty;
        int mtz=globale.mtz;
        int nb_plats=globale.nb_plats;
        int[, ,] mape = new int[mtx, mty, mtz];
        Vector3 player_spawn=new Vector3(mtx/2,mty/2+2,mtz/2)*dc;
        Vector3 pos_finniv;
        Vector3 act_platform_pos=new Vector3(mtx/2,mty/2,mtz/2);
        Spatial level=new Spatial();
        int max_security=100;
        int security=0;
        int final_nb_plats=0;
        int dx=0,dy=0,dz=0;
        //création
        for(int w=0; w<nb_plats; w++){
            //initialisation des variables
            security=0;
            Vector3 dec = new Vector3(dx,dy,dz);
            //création de la platforme
            final_nb_plats=w;
            act_platform_pos=act_platform_pos+dec;
            PackedScene packedScene = (PackedScene)ResourceLoader.Load("res://assets/floor.tscn");
            MeshInstance floor=(MeshInstance)packedScene.Instance();
            level.AddChild(floor);
            floor.Translation = act_platform_pos*dc;
            floor.Scale=tc;            
            mape[(int)act_platform_pos.x,(int)act_platform_pos.y,(int)act_platform_pos.z]=1;
            //preparation prochaine platforme
            dx=0;
            dy=0;
            dz=0;
            do{
                security++;
                (dx,dy,dz)=decalage_platforms(tc);
                if(act_platform_pos.x+dx<0){ dx=Convert.ToInt32(-act_platform_pos.x); }
                if(act_platform_pos.z+dz<0){ dz=Convert.ToInt32(-act_platform_pos.z); }
                if(act_platform_pos.y+dy<0){ dy=Convert.ToInt32(-act_platform_pos.y); }
                if(act_platform_pos.x+dx>=mtx){ dx=Convert.ToInt32(mtx-1-act_platform_pos.x); }
                if(act_platform_pos.z+dz>=mtz){ dz=Convert.ToInt32(mty-1-act_platform_pos.z); }
                if(act_platform_pos.y+dy>=mty){ dy=Convert.ToInt32(mtz-1-act_platform_pos.y); }
                dec = new Vector3(dx,dy,dz);
                
            }while(test_derange(act_platform_pos+dec,mape,mtx,mty,mtz) && security<max_security); //on aimerait que le niveau soit possible
            //test de sécurité
            if(security==max_security){
                break;
            }
        }
        //
        pos_finniv=new Vector3(act_platform_pos.x,act_platform_pos.y+2,act_platform_pos.z)*dc;
        //
        return (level,player_spawn,pos_finniv,final_nb_plats);
    }






}
