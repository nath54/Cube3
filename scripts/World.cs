using System.Collections.Generic;
using Godot;
using System;

public class World : Spatial
{
    
    private Control pause_menu;
    private Player player;
    private Spatial finNiv;
    private Area finNivArea;
    private Control loading;
    private UI_In_game ui_in_game;
    public float timeLeft; //In seconds
    public float timeTotal; //In seconds
    public Timer timer;
    public GridMap gridMap;
    public string tipemap = "maze";
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        globale = (Global)GetNode("/root/Global");
        //
        player= (Player)GetNode("Player");
        player.Connect("onPauseBtPress", this, nameof(Pause));
        //
        finNiv = (Spatial)GetNode("FinNiv");
        finNivArea = (Area)GetNode("FinNiv/Area");
        //
        gridMap = (GridMap)GetNode("GridMap");
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
            finNiv.Translation = new Vector3((gridMap.finx*gridMap.CellSize.x)+gridMap.CellSize.x/2, ((gridMap.finy+1.5F)*gridMap.CellSize.y)+finNiv.Scale.y/10, (gridMap.finz*gridMap.CellSize.z)+gridMap.CellSize.z/2);
        }
        else if(gridMap.tipe=="maze"){
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
            finNiv.Translation = new Vector3((gridMap.finx*gridMap.CellSize.x)+gridMap.CellSize.x/2, ((gridMap.finy+1.5F)*gridMap.CellSize.y)+finNiv.Scale.y/10, (gridMap.finz*gridMap.CellSize.z)+gridMap.CellSize.z/2);
        }
        //
        loading= (Control)GetNode("Loading");
        loading.Visible=false;
        //
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        if(!player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Captured); }
        //
        ui_in_game=(UI_In_game)GetNode("UI_In_game");
        ui_in_game.changeLevelText(globale.level);
        //
        timer=(Timer)GetNode("time_seconds");
        timer.Start();
        timeTotal=60;
        timeLeft=timeTotal;
    }

    public void nivFini(){
        globale.level+=1;
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void onPauseMenuBtResumePress(){
        Resume();
    }

    public void Resume(){
        pause_menu.Visible=false;
        if(!player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Captured); }
        player.paused=false;
        ui_in_game.changeDebugText("visible : "+Convert.ToString(pause_menu.Visible));
    }

    public void Pause(){
        pause_menu.Visible=true;
        if(!player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
        player.paused=true;
        ui_in_game.changeDebugText("visible : "+Convert.ToString(pause_menu.Visible));
    }

    public override void _Input(InputEvent @event){
        if(Input.IsActionJustPressed("menu")){
            if(pause_menu.Visible){
                Resume();
            }
            else{
                Pause();
            }
        }
        if(Input.IsActionJustPressed("debug")){
            GD.Print("player translation = x : "+player.Translation.x+" z : "+player.Translation.z);
        }
    }

   

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(finNivArea.OverlapsBody(player)){
            nivFini();
        }
    }

    public void _on_time_seconds_timeout(){
        if(!player.paused){
            timeLeft-=timer.WaitTime;
            ui_in_game.changePercentBar(timeLeft/timeTotal*100);
        }
        timer.Start();
        if(timeLeft<=0){
            GetTree().ChangeScene("res://menus/MenuPerdu.tscn");
        }
    }

    


}

