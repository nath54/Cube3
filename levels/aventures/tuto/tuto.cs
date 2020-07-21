using Godot;
using System;

public class tuto : Spatial
{
    public Global globale;
    private Player player;
    
    public finNiv finNiv;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        globale.Connect("playerDeath",this,nameof(player_mort));
        //
        player=(Player)GetNode("Player");
        player.Connect("onPauseBtPress", this, nameof(pause));
        player.setSkin(globale.skin_id_equipe);
        //
        finNiv = (finNiv)GetNode("finNiv");
        finNiv.Connect("bodyTouched", this, nameof(onFinNiv));
    }

    public void player_mort(){

    }

    public void pause(){

    }
    public void onFinNiv(){

    }

}
