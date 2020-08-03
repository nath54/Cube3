using System.Collections.Generic;
using Godot;
using System;

public class World : Spatial
{
    
    private Player player;
    public finNiv finNiv;
    public GridMap gridMap;
    public string tipemap = "maze";
    public Global globale;
    public WorldEnvironment environment;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        globale = (Global)GetNode("/root/Global");
        //
        player= (Player)GetNode("Player");
        finNiv = (finNiv)GetNode("finNiv");
        //
        gridMap = (GridMap)GetNode("GridMap");
        gridMap.worlde = this;
        gridMap.tipe=globale.tipe;
        gridMap.tx=globale.mtx;
        gridMap.ty=globale.mty;
        gridMap.tz=globale.mtz;        
        gridMap.nbchem=globale.nbchem;
        gridMap.nb_plats=globale.nb_plats;
        gridMap.CellSize = globale.grid_scale;
        gridMap.CellScale=globale.grid_cell_scale;
        gridMap.generate();
        if(gridMap.tipe=="platforms"){
            GD.Print(" x : "+gridMap.depx+" , z : "+gridMap.depz);
            player.Translation = new Vector3((gridMap.depx*gridMap.CellSize.x)+gridMap.CellSize.x/2, (gridMap.depy*gridMap.CellSize.z)+player.Scale.y+1, (gridMap.depz*gridMap.CellSize.z)+gridMap.CellSize.z/2);
            player.spawnpoint=player.Translation;
            GD.Print("SPAWN = x : "+player.Translation.x+" , z : "+player.Translation.z);
            player.taille = globale.player_taille;
            player.Scale = new Vector3(player.taille,player.taille,player.taille);
            if(player.taille>0.7){
                player.JUMP_FORCE*=player.taille*1F;
                player.MOVE_SPEED*=player.taille*1F;
            }
            else if(player.taille>0.5){
                player.JUMP_FORCE*=player.taille*1.2F;
                player.MOVE_SPEED*=player.taille*1.2F;
            }
            else{
                player.JUMP_FORCE*=player.taille*1.5F;
                player.MOVE_SPEED*=player.taille*1.5F;
            }
            finNiv.Scale=player.Scale*2;
            finNiv.Translation = new Vector3((gridMap.finx*gridMap.CellSize.x)+gridMap.CellSize.x/2, ((gridMap.finy+1.5F)*gridMap.CellSize.y)+finNiv.Scale.y, (gridMap.finz*gridMap.CellSize.z)+gridMap.CellSize.z/2);
        }
        else if(gridMap.tipe=="maze"){
            GD.Print(" x : "+gridMap.depx+" , z : "+gridMap.depz);
            player.Translation = new Vector3((gridMap.depx*gridMap.CellSize.x)-player.Scale.x, (gridMap.depy*gridMap.CellSize.z)+player.Scale.y+1, (gridMap.depz*gridMap.CellSize.z)-player.Scale.z);
            player.spawnpoint=player.Translation;
            GD.Print("SPAWN = x : "+player.Translation.x+" , z : "+player.Translation.z);
            player.taille = globale.player_taille;
            player.Scale = new Vector3(player.taille,player.taille,player.taille);
            if(player.taille>0.7){
                player.JUMP_FORCE*=player.taille*1F;
                player.MOVE_SPEED*=player.taille*1F;
            }
            else if(player.taille>0.5){
                player.JUMP_FORCE*=player.taille*1.2F;
                player.MOVE_SPEED*=player.taille*1.2F;
            }
            else{
                player.JUMP_FORCE*=player.taille*1.5F;
                player.MOVE_SPEED*=player.taille*1.5F;
            }
            finNiv.Scale=player.Scale*2;
            finNiv.Translation = new Vector3((gridMap.finx*gridMap.CellSize.x)+gridMap.CellSize.x/2, ((gridMap.finy)*gridMap.CellSize.y)+finNiv.Scale.y/10, (gridMap.finz*gridMap.CellSize.z)+gridMap.CellSize.z/2);
        }
        //time
        globale.levele.timeTotal=gridMap.final_nb_plats*5;
        //
        if(globale.difficulty==0){ globale.levele.timeTotal*=1.5F; }
        else if(globale.difficulty==2){ globale.levele.timeTotal/=2; }
        else if(globale.difficulty==3){ globale.levele.timeTotal/=3; }
        //
        //GD.Print("world time ",globale.levele.timeTotal);
        globale.levele.timeLeft=globale.levele.timeTotal;
        //
    }


    


}

