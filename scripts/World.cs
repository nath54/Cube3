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
    private global_arcade global_Arcade;
    private UI_In_game ui_in_game;
    public float timeLeft; //In seconds
    public float timeTotal; //In seconds
    public Timer timer;
    public GridMap gridMap;
    public string tipemap = "maze";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        global_Arcade = (global_arcade)GetNode("/root/GlobalArcade");
        //
        player= (Player)GetNode("Player");
        player.Connect("onPauseBtPress", this, nameof(Pause));
        //
        finNiv = (Spatial)GetNode("FinNiv");
        finNivArea = (Area)GetNode("FinNiv/Area");
        //
        gridMap = (GridMap)GetNode("GridMap");
        gridMap.tipe=tipemap;
        gridMap.tx=32;
        gridMap.ty=32;
        gridMap.tz=32;
        gridMap.generate();
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
        //
        timer=(Timer)GetNode("time_seconds");
        timer.Start();
        timeTotal=60;
        timeLeft=timeTotal;
    }

    public void nivFini(){
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
    }

   

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(finNivArea.OverlapsBody(player)){
            nivFini();
        }
    }

    public void _on_time_seconds_timeout(){
        timeLeft-=timer.WaitTime;
        ui_in_game.changePercentBar(timeLeft/timeTotal*100);
        timer.Start();
        if(timeLeft<=0){
            GetTree().ChangeScene("res://menus/MenuPerdu.tscn");
        }
    }




}

