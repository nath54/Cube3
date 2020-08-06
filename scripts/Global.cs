using System.Linq;
using System.Collections.Generic;
using Godot;
using System;

public class Global : Node
{
    
    public int level=1;
    public int mtx = 100;
    public int mtz = 100;
    public int mty = 100;
    public int nbchem=32;
    public int ncubes=0;
    public float timemax=180;
    public int nb_plats=20;
    public string tipe="platforms";
    public float player_taille=1;
    public Vector3 grid_scale = new Vector3(1,1,1);
    public float grid_cell_scale = 1;
    public int skin_id_equipe=0;
    public int max_skin=9;
    public int ms_cam=0;
    public int highscore_plats=1;
    public int highscore_easy=1;
    public int highscore_normal=1;
    public int highscore_hard=1;
    public int highscore_hell=1;
    public bool aff_fps=true;
    public bool sao=false;
    public int sao_quality=0;
    public int actu_id_niv;
    public int difficulty=0; //0=easy 1=normal 2=hard 3=hell
    public string[] difs = {"easy","normal","hard","hell"};
    public bool respawn=true;
    public string vidpath="";
    public string vidscenepath="res://menus/MainMenu.tscn";
    public string[] rarities = {"common","rare","epic","legendary","divine","mythical"};
    public int[] rar_deb_skin = {2,10,25,50,80,100};
    public string[] cl_rars={"92cfff","fda10b","460771","00fd0c","00fd0c","ff0201"};
    //public int cam_max_view_distance = 100;
    public float glow_intensity=1;
    public float glow_strength=0.96F;
    public float saturation=1;
    public float luminosity=1;
    public float cam_far=100;
    // skins

    public Dictionary<string, object> quick_saved_game = new Dictionary<string, object>(){
        {"name","quick_saved_game"},
        {"saved", false},
        {"difficulty",0},
        {"level",0},
        {"tipe","platforms"},
    };

    public List<List<string>> messages_queue = new List<List<string>>();
    public List<List<string>> messages_ncubes_queue = new List<List<string>>();
    public List<List<string>> messages_skins_queue = new List<List<string>>();

    public string[] skins_names={
        "base",
        "smile",
        "smileye",
        "ssj",
        "carreaux",
        "furnace",
        "halo",
        "transblue",
        "ncubes",
    };
    public string[] skins_preview={
        "res://img_skins/base.png",
        "res://img_skins/smile.png",
        "res://img_skins/smileye.png",
        "res://img_skins/ssj.png",
        "res://img_skins/carreaux.png",
        "res://img_skins/furnace.png",
        "res://img_skins/halo.png",
        "res://img_skins/transp_blue.png",
        "res://img_skins/ncubes.png",
    };
    public string[] skins_path={
        "res://player/skins/skin_0.tscn",
        "res://player/skins/skin_1.tscn",
        "res://player/skins/skin_2.tscn",
        "res://player/skins/skin_3.tscn",
        "res://player/skins/skin_4.tscn",
        "res://player/skins/skin_5.tscn",
        "res://player/skins/skin_6.tscn",
        "res://player/skins/skin_7.tscn",
        "res://player/skins/skin_8.tscn",
    };
    public bool[] skins_unlocked={
        true,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
    };
    public bool[] skins_secret={
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
    };

    public int[] skins_rarity={ //0=common 1=rare 2=epic 3=legendary 4=divine 5=mythical
        0,
        0,
        0,
        3,
        1,
        1,
        2,
        2,
        3,
    };

    public string[] skins_recup={
        "none",
        "arcade",
        "arcade",
        "arcade",
        "arcade",
        "arcade",
        "arcade",
        "arcade",
        "pay",
    };

    // cost if recup="pay", level if recup="level", else 0
    public int[] skins_value={
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        9999,
    };

    //levels
    public string[] levels_names={
        "tuto-1",
        "tuto-2",
        "tuto-3",
        "tuto-4",
        "tuto-5",
    };
    public string[] levels_category={
        "tutos",
        "tutos",
        "tutos",
        "tutos",
        "tutos",
    };
    public string[] levels_path={
        "res://levels/aventures/tuto/tuto1.tscn",
        "res://levels/aventures/tuto/tuto2.tscn",
        "res://levels/aventures/tuto/tuto3.tscn",
        "res://levels/aventures/tuto/tuto4.tscn",
        "res://levels/aventures/tuto/tuto5.tscn",
    };
    public int[] levels_requirements={ //id of required level, -1 = not required
        -1,
        0,
        1,
        2,
        3,
    };
    public bool[] levels_finis={
        false,
        false,
        false,
        false,
        false,
    };
    public float[] levels_time={
        80,
        80,
        80,
        80,
        80,
    };
    public int[] levels_recomp_ncubes={
        10,
        20,
        30,
        40,
        50,
    };
    //

    public string actu_cat="";

    public Player player;
    public level levele;
    public finNiv finnive;
    public Timer timer_message;
    public string controller_mode="keyboard";

    [Signal]
    public delegate void playerDeath();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        LoadGame();
        //
        timer_message=new Timer();
        timer_message.WaitTime=0.2F;
        AddChild(timer_message);
        timer_message.Connect("timeout", this, nameof(on_timer_message));
        timer_message.Start();
    }

    public void on_timer_message(){
        while(messages_queue.Count>0){
            List<string> m = messages_queue[0];
            aff_message(m[0],m[1],m[2]);
            messages_queue.RemoveAt(0);
        }
        while(messages_ncubes_queue.Count>0){
            List<string> m = messages_ncubes_queue[0];
            aff_message_ncubes(m[0],Convert.ToInt32(m[1]),m[2]);
            messages_ncubes_queue.RemoveAt(0);
        }
        while(messages_skins_queue.Count>0){
            List<string> m = messages_skins_queue[0];
            aff_message_skins(m[0],Convert.ToInt32(m[1]),m[2]);
            messages_skins_queue.RemoveAt(0);
        }
        timer_message.Start();
    }

    public void player_death(){
        EmitSignal("playerDeath");
    }
    public bool is_mobile(){
		return (OS.GetName()=="Android" || OS.GetName()=="iOS");
	}

    //fonction pour débugger
    public void affNode(Node node, int iterations){
        string txt="  -";
        string tt="";
        for(int i=0; i<=iterations; i++){ txt="  "+txt; }
        if(node is Spatial sn){ tt=" ( x:"+sn.Translation.x+", y:"+sn.Translation.y+", z:"+sn.Translation.z+")"; }
        GD.Print(txt,node," : ", node.Name,tt);
        Godot.Collections.Array children = node.GetChildren();
        foreach(Node n in children){
            affNode(n,iterations+1);
        }

    }

    public Godot.Collections.Dictionary<string, object> Save()
    {
    return new Godot.Collections.Dictionary<string, object>()
        {
            { "name", "Global" },
            { "Filename", Filename },
            { "Parent", GetParent().GetPath() },
            { "skin_id_equipe",""+skin_id_equipe },
            { "ms_cam", ""+ms_cam },
            { "highscore_easy", ""+highscore_easy },
            { "highscore_normal", ""+highscore_normal },
            { "highscore_hard", ""+highscore_hard },
            { "highscore_hell", ""+highscore_hell },
            { "aff_fps", aff_fps},
            { "sao", sao},
            { "sao_quality", sao_quality},
            { "difficulty",difficulty},
            { "ncubes",ncubes },
            { "glow_intensity",glow_intensity},
            { "glow_strength",glow_strength},
            { "saturation",saturation},
            { "luminosity",luminosity},
            { "cam_far",cam_far},
            { "controller_mode",controller_mode}
        };
    }

    public Godot.Collections.Dictionary<string, object> SaveSkins()
    {

        Godot.Collections.Dictionary<string, object> dic = new Godot.Collections.Dictionary<string, object>()
        {
            { "name", "skin_unlocked" },
        };

        for(int ids=0;ids<skins_unlocked.Length; ids++){
            dic[""+ids]=skins_unlocked[ids];
        }

        return dic;
    }
    
    public Godot.Collections.Dictionary<string, object> SaveLevels()
    {

        Godot.Collections.Dictionary<string, object> dic = new Godot.Collections.Dictionary<string, object>()
        {
            { "name", "levels_finis" },
        };

        for(int idl=0;idl<levels_finis.Length; idl++){
            dic[""+idl]=levels_finis[idl];
        }

        return dic;
    }

    public void SaveGame(){
        var saveGame = new File();
        saveGame.Open("user://savegame.save", (Godot.File.ModeFlags)File.ModeFlags.Write);
        
        //on sauvegarde le global
        var nodeData=Save();
        var nodeDataSkins=SaveSkins();
        var nodeDataLevels=SaveLevels();
        saveGame.StoreLine(JSON.Print(nodeData));
        saveGame.StoreLine(JSON.Print(nodeDataSkins));
        saveGame.StoreLine(JSON.Print(nodeDataLevels));
        saveGame.StoreLine(JSON.Print(quick_saved_game));
        saveGame.Close();
    }

    public void LoadGame(){
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save"))
            return; // Error!  We don't have a save to load.

        // We need to revert the game state so we're not cloning objects during loading.
        // This will vary wildly depending on the needs of a project, so take care with
        // this step.
        // For our example, we will accomplish this by deleting saveable objects.
        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
            saveNode.QueueFree();

        // Load the file line by line and process that dictionary to restore the object
        // it represents.
        saveGame.Open("user://savegame.save", (Godot.File.ModeFlags)File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            if((string)nodeData["name"]=="Global"){
                if( nodeData.Keys.Contains("skin_id_equipe")){
                    skin_id_equipe=Convert.ToInt32(nodeData["skin_id_equipe"]);
                }
                if( nodeData.Keys.Contains("ms_cam")){
                    ms_cam=Convert.ToInt32(nodeData["ms_cam"]);
                }
                if( nodeData.Keys.Contains("highscore_easy")){
                    highscore_easy=Convert.ToInt32(nodeData["highscore_easy"]);
                }
                if( nodeData.Keys.Contains("highscore_normal")){
                    highscore_normal=Convert.ToInt32(nodeData["highscore_normal"]);
                }
                if( nodeData.Keys.Contains("highscore_hard")){
                    highscore_hard=Convert.ToInt32(nodeData["highscore_hard"]);
                }
                if( nodeData.Keys.Contains("highscore_hell")){
                    highscore_hell=Convert.ToInt32(nodeData["highscore_hell"]);
                }
                if( nodeData.Keys.Contains("aff_fps")){
                    aff_fps=Convert.ToBoolean(nodeData["aff_fps"]);
                }
                if( nodeData.Keys.Contains("sao")){
                    sao=Convert.ToBoolean(nodeData["sao"]);
                }
                if( nodeData.Keys.Contains("sao_quality")){
                    sao_quality=Convert.ToInt32(nodeData["sao_quality"]);
                }
                if( nodeData.Keys.Contains("difficulty")){
                    difficulty=Convert.ToInt32(nodeData["difficulty"]);
                }
                if( nodeData.Keys.Contains("ncubes")){
                    ncubes=Convert.ToInt32(nodeData["ncubes"]);
                }
                if( nodeData.Keys.Contains("glow_intensity")){
                    glow_intensity=(float)Convert.ToDouble(nodeData["glow_intensity"]);
                }
                if( nodeData.Keys.Contains("glow_strength")){
                    glow_strength=(float)Convert.ToDouble(nodeData["glow_strength"]);
                }
                if( nodeData.Keys.Contains("saturation")){
                    saturation=(float)Convert.ToDouble(nodeData["saturation"]);
                }
                if( nodeData.Keys.Contains("luminosity")){
                    luminosity=(float)Convert.ToDouble(nodeData["luminosity"]);
                }
                if( nodeData.Keys.Contains("cam_far")){
                    cam_far=(float)Convert.ToDouble(nodeData["cam_far"]);
                }
                if( nodeData.Keys.Contains("controller_mode")){
                    controller_mode=Convert.ToString(nodeData["controller_mode"]);
                }
            }
            else if((string)nodeData["name"]=="skin_unlocked"){
                foreach(string key in nodeData.Keys){
                    int ids;
                    if(int.TryParse(key, out ids) && ids<skins_unlocked.Length){
                        skins_unlocked[ids]=(Boolean)nodeData[key];
                    }
                }
            }
            else if((string)nodeData["name"]=="levels_finis"){
                foreach(string key in nodeData.Keys){
                    int idl;
                    if(int.TryParse(key, out idl) && idl<levels_finis.Length){
                        levels_finis[idl]=(Boolean)nodeData[key];
                    }
                }
            }
            else if((string)nodeData["name"]=="quick_saved_game"){
                foreach(string key in nodeData.Keys){
                    quick_saved_game[key]=nodeData[key];
                }
            }
            
        }
        saveGame.Close();
    }

    public void aff_message(string titre="titre",string message="message",string txt_but="ok"){
        PackedScene packed = ResourceLoader.Load("res://menus/Message.tscn") as PackedScene;
        Message mes = (Message)packed.Instance();
        mes.titre=titre;
        mes.message=message;
        mes.txt_bt=txt_but;        
        GetTree().Root.AddChild(mes);
    }

    public void aff_message_ncubes(string titre="You gain : ",int nbcubes=0,string txt_but="ok"){
        PackedScene packed = ResourceLoader.Load("res://menus/Message_ncubes.tscn") as PackedScene;
        Message mes = (Message)packed.Instance();
        mes.titre=titre;
        mes.message=""+nbcubes;
        mes.txt_bt=txt_but;
        GetTree().Root.AddChild(mes);
    }
    
    public void aff_message_skins(string titre="You unlock this skin !",int idskin=0,string txt_but="ok"){
        PackedScene packed = ResourceLoader.Load("res://menus/Message_ncubes.tscn") as PackedScene;
        Message_skin mes = (Message_skin)packed.Instance();
        mes.titre=titre;
        mes.idskin=idskin;
        mes.txt_bt=txt_but;
        GetTree().Root.AddChild(mes);
    }

    public void quick_save(){
        quick_saved_game["saved"]=true;
        quick_saved_game["level"]=level;
        quick_saved_game["tipe"]=tipe;
        quick_saved_game["difficulty"]=difficulty;
        SaveGame();
    }


}
