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
    public GenerateArcade generateArcade;
    public WorldEnvironment environment;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        globale = (Global)GetNode("/root/Global");
        PackedScene pack = ResourceLoader.Load("res://libs/GenerateArcade.tscn") as PackedScene;
        generateArcade = (GenerateArcade)pack.Instance();
        AddChild(generateArcade);
        //
        player= (Player)GetNode("Player");
        finNiv = (finNiv)GetNode("finNiv");
        int finale_nb_plats=20;
        if(globale.tipe=="platforms"){
            Spatial mape;
            Vector3 player_pos;
            Vector3 finniv_pos;    
            (mape,player_pos,finniv_pos,finale_nb_plats) = generateArcade.generate_platforms_1();
            player.Translation = player_pos;
            player.spawnpoint=player.Translation;
            finNiv.Translation = finniv_pos;
            AddChild(mape);
        }
         if(globale.tipe=="maze"){
            Spatial mape;
            Vector3 player_pos;
            Vector3 finniv_pos;    
            (mape,player_pos,finniv_pos,finale_nb_plats) = generateArcade.generate_maze_1();
            player.Translation = player_pos;
            player.spawnpoint=player.Translation;
            finNiv.Translation = finniv_pos;
            AddChild(mape);
        }
        //time
        globale.levele.timeTotal=finale_nb_plats*6;
        //
        if(globale.difficulty==0){ globale.levele.timeTotal*=1.5F; }
        else if(globale.difficulty==2){ globale.levele.timeTotal/=1.75F; }
        else if(globale.difficulty==3){ globale.levele.timeTotal/=2.5F; }
        //
        //GD.Print("world time ",globale.levele.timeTotal);
        globale.levele.timeLeft=globale.levele.timeTotal;
        //
    }


    


}

