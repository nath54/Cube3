using System.Linq;
using Godot;
using System;

public class PreArcadeLevel : Control
{
    Global globale;
    AnimationPlayer animationPlayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale = (Global)GetNode("/root/Global");
        //
        animationPlayer=(AnimationPlayer)GetNode("L_click/AnimationPlayer");
        animationPlayer.CurrentAnimation="clignotement";
        //
        if((bool)globale.quick_saved_game["saved"]==true && globale.quick_saved_game.Keys.Contains("tipe") && globale.quick_saved_game.Keys.Contains("level") && globale.quick_saved_game.Keys.Contains("difficulty")){
            globale.tipe=Convert.ToString(globale.quick_saved_game["tipe"]);
            globale.level=Convert.ToInt32(globale.quick_saved_game["level"]);
            globale.difficulty=Convert.ToInt32(globale.quick_saved_game["difficulty"]);
            //on supprime la sauvegarde
            globale.quick_saved_game["saved"]=false;
        }
        else{
            string[] tps={"platforms"};
            Random rand = new Random();
            globale.tipe=tps[rand.Next(0,tps.Length)];
        }
        if(globale.tipe=="platforms"){ prepare_platform(); }
        if(globale.tipe=="maze"){ prepare_maze(); }
        //
        Label l_dif = (Label)GetNode("L_difficulty");
        Label l_time = (Label)GetNode("L_time");
        Label l_titre = (Label)GetNode("Titre");
        Label l_click = (Label)GetNode("L_click");
        //
        l_dif.Text = "Difficulty : "+globale.difs[globale.difficulty];
        l_titre.Text = "level "+globale.level+" : "+globale.tipe;
        l_time.Text = "time maximum : "+globale.timemax+" sec";
        //
        l_click.Text="click or press "+((InputEvent)InputMap.GetActionList("jump")[0]).AsText()+" to play ...";
        //
        bool gagne_skin=false;
        int rar=0;
        for(int w=0; w<globale.max_skin; w++){
            if(globale.skins_recup[w]=="arcade" || globale.skins_recup[w]=="arcade-"+globale.difs[globale.difficulty] ){
                if(!globale.skins_unlocked[w] && globale.level>=globale.rar_deb_skin[globale.skins_rarity[w]]){
                    gagne_skin=true;
                    if(globale.skins_rarity[w]>rar){
                        rar=globale.skins_rarity[w];
                    }
                }
            }
        }
        if(gagne_skin){
            Panel ifd = (Panel)GetNode("Info_die");
            Label lifd = (Label)GetNode("Info_die/L_srar");
            Godot.Color cl = new Godot.Color(globale.cl_rars[rar]);  //;
            lifd.Text=globale.rarities[rar];
            lifd.AddColorOverride("font_color",cl);
            ifd.Visible=true;
        }

    }

    public void prepare_platform(){
        globale.mtx=80+5*globale.level;
        globale.mtz=80+5*globale.level;
        globale.mty=80+5*globale.level;
        globale.nb_plats=10+globale.level;
        globale.tipe="platforms";
        //
        globale.timemax=globale.nb_plats*6;
        //
        if(globale.difficulty==0){ globale.timemax*=1.5F; }
        else if(globale.difficulty==2){ globale.timemax/=1.75F; }
        else if(globale.difficulty==3){ globale.timemax/=2.5F; }
    }

    public void prepare_maze(){
        globale.mtx=20+5*globale.level;
        globale.mtz=20+5*globale.level;
        globale.mty=2;
        globale.nb_plats=6+globale.level*2;
        globale.tipe="maze";
        //
        globale.timemax=globale.nb_plats*6;
        //
        if(globale.difficulty==0){ globale.timemax*=1.5F; }
        else if(globale.difficulty==2){ globale.timemax/=1.75F; }
        else if(globale.difficulty==3){ globale.timemax/=2.5F; }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {               
        if(!animationPlayer.IsPlaying()){
            animationPlayer.Play();
        }            
    }

    public override void _Input(InputEvent @event){
        if(@event is InputEventScreenTouch){
            TextureButton tbt = (TextureButton)GetNode("Bt_saveandexit");
            if(!tbt.IsHovered()){
                globale.chargement("res://levels/World.tscn");
                //GetTree().ChangeScene("res://levels/World.tscn");
            }
        }
        else if(@event is InputEventKey ie){
            if(Input.IsActionPressed("jump")){
                globale.chargement("res://levels/World.tscn");
                //GetTree().ChangeScene("res://levels/World.tscn");
            }
        }
    }

    public void _on_Bt_saveandexit_pressed(){
        globale.quick_save();
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
