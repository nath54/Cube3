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
    public string[] wtps = {"maze","platforms"};

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale=(Global)GetNode("/root/Global");
        globale.levele=this;
        globale.Connect("playerDeath",this,nameof(player_mort));
        if( globale.tipe=="platforms" && globale.level>globale.highscore_plats){
            globale.highscore_plats=globale.level;
            globale.SaveGame();
        }
        if(globale.difficulty<3){
            globale.respawn=true;
        }
        else{
            globale.respawn=false;
        }
        //
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        if(!globale.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Captured); }
        //
        ui_in_game=(UI_In_game)GetNode("UI_In_game");
        ui_in_game.changeLevelText(globale.level);
        //
        timer=(Timer)GetNode("time_seconds");
        timer.Connect("timeout", this, nameof(_on_time_seconds_timeout));
        timer.Start();
        if(wtps.Contains(globale.tipe)){
            timeTotal=globale.timemax;
            //Ne sert un peu a rien, vu que dans world.cs, on remet le temps, mais bon je l'ai quand meme laissé au cas ou.
        }else{
            timeTotal=globale.levels_time[globale.actu_id_niv];
        }
        //difficulty
        if(globale.difficulty==0){ timeTotal*=1.5F; }
        else if(globale.difficulty==2){ timeTotal/=1.5F; }
        else if(globale.difficulty==3){ timeTotal/=2F; }
        //
        timeLeft=timeTotal;
        //
        
        if(!(wtps.Contains(globale.tipe))){
            ui_in_game.fps_counter.Visible=false;
        }
        //
        globale.finnive.Connect("bodyTouched", this, nameof(onFinNiv));
        globale.player.Connect("onPauseBtPress", this, nameof(Pause));
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

    public void partiePerdue(){
        if(!globale.player.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
        GetTree().ChangeScene("res://menus/MenuPerdu.tscn");
    }

    public void _on_time_seconds_timeout(){
        if(!globale.player.paused){
            //GD.Print("level time max ",timeTotal);
            timeLeft-=timer.WaitTime;
            ui_in_game.changePercentBar(timeLeft/timeTotal*100);
        }
        timer.Start();
        if(timeLeft<=0){
            partiePerdue();
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
