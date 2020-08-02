using System.Linq;
using Godot;
using System;

public class level : Node
{
    private Control pause_menu;

    private UI_In_game ui_in_game;
    public Timer timer;
    public float timeLeft; //In seconds
    public float timeTotal; //In seconds
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale=(Global)GetNode("/root/Global");
        //
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Captured); }
        //
        ui_in_game=(UI_In_game)GetNode("UI_In_game");
        ui_in_game.changeLevelText(globale.level);
        //
        timer=(Timer)GetNode("time_seconds");
        timer.Start();
        timeTotal=globale.timemax;
        timeLeft=timeTotal;
        //
        string[] aa = {"maze","platforms"};
        if(!(aa.Contains(globale.tipe))){
            ui_in_game.fps_counter.Visible=false;
        }
        //
        globale.finnive.Connect("bodyTouched", this, nameof(onFinNiv));
    }


    public void onPauseMenuBtResumePress(){
        Resume();
    }

    public void player_mort(){
        globale.player.playerDeath();
    }

    public void Resume(){
        pause_menu.Visible=false;
        if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Captured); }
        globale.player.paused=false;
        //ui_in_game.changeDebugText("visible : "+Convert.ToString(pause_menu.Visible));
    }

    public void Pause(){
        pause_menu.Visible=true;
        if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
        globale.player.paused=true;
        //ui_in_game.changeDebugText("visible : "+Convert.ToString(pause_menu.Visible));
    }

    public override void _Input(InputEvent @event){
        if(Input.IsActionJustPressed("menu")){
            if(pause_menu.Visible==true){
                Resume();
            }
            else{
                Pause();
            }
        }
        if(Input.IsActionJustPressed("debug")){
            GD.Print("player translation = x : "+globale.player.Translation.x+" z : "+globale.player.Translation.z);
        }
    }

    public void onFinNiv(Node body){
        if(body is Player){
            nivFini();
        }
    }

    public void _on_time_seconds_timeout(){
        if(!globale.player.paused){
            timeLeft-=timer.WaitTime;
            ui_in_game.changePercentBar(timeLeft/timeTotal*100);
        }
        timer.Start();
        if(timeLeft<=0){
            if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
            GetTree().ChangeScene("res://menus/MenuPerdu.tscn");
        }
    }

    public void nivFini(){
        string[] worlds = {"maze","platforms"};
        if(worlds.Contains(globale.tipe)){
            globale.level+=1;
            if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
            GetTree().ChangeScene("res://levels/World.tscn");
        }
        else if(globale.tipe=="levels"){
            if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
            globale.levels_finis[globale.actu_id_niv]=true;
            GetTree().ChangeScene("res://menus/MenuLevels.tscn");
        }
    }
}
