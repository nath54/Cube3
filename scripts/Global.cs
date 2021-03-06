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
    public int p_fixes_r=0;
    public Vector3 grid_scale = new Vector3(1,1,1);
    public float grid_cell_scale = 1;
    public int skin_id_equipe=0;
    public int max_skin=16;
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
    public Godot.Environment camenv;
    // skins

    public Dictionary<string, Dictionary<string, string>> themes_objects = new Dictionary<string, Dictionary<string, string>>(){
        {"lava",new Dictionary<string, string>(){{"floor","res://themes/lava/floor.tscn"},}},
        {"lava_rock", new Dictionary<string, string>(){{"floor","res://themes/lava_rock/floor.tscn"},}},
        {"rock_lava",new Dictionary<string, string>(){{"floor","res://themes/rock_lava/floor.tscn"},}},
        {"stone_dirt",new Dictionary<string, string>(){{"floor","res://themes/stone_dirt/floor.tscn"},}},
        {"grass",new Dictionary<string, string>(){{"floor","res://assets/floor.tscn"},}},
        {"normal",new Dictionary<string, string>(){{"floor","res://assets/floor.tscn"},}},
    };

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
        "base", //0
        "smile",  //1
        "smileye",  //2
        "ssj",  //3
        "carreaux",  //4
        "furnace",  //5
        "halo",  //6
        "transblue",  //7
        "ncubes",  //8
        "ninja",  //9
        "france",  //10
        "de",  //11
        "de_noir",  //12
        "redline",  //13
        "cubeman", //14
        "pumpkin", //15
    };
    public string[] skins_preview={
        "res://img_skins/base.png", //0
        "res://img_skins/smile.png", //1
        "res://img_skins/smileye.png", //2
        "res://img_skins/ssj.png", //3
        "res://img_skins/carreaux.png", //4
        "res://img_skins/furnace.png", //5
        "res://img_skins/halo.png", //6
        "res://img_skins/transp_blue.png", //7
        "res://img_skins/ncubes.png", //8
        "res://img_skins/ninja.png", //9
        "res://img_skins/france.png", //10
        "res://img_skins/de.png", //11
        "res://img_skins/de_noir.png", //12
        "res://img_skins/redline.png", //13
        "res://img_skins/cubeman.png", //14
        "res://img_skins/pumpkin.png", //15
    };
    public string[] skins_path={
        "res://player/skins/skin_0.tscn", //0
        "res://player/skins/skin_1.tscn", //1
        "res://player/skins/skin_2.tscn", //2
        "res://player/skins/skin_3.tscn", //3
        "res://player/skins/skin_4.tscn", //4
        "res://player/skins/skin_5.tscn", //5
        "res://player/skins/skin_6.tscn", //6
        "res://player/skins/skin_7.tscn", //7
        "res://player/skins/skin_8.tscn", //8
        "res://player/skins/skin_9.tscn", //9
        "res://player/skins/skin_10.tscn", //10
        "res://player/skins/skin_11.tscn", //11
        "res://player/skins/skin_12.tscn", //12
        "res://player/skins/skin_13.tscn", //13
        "res://player/skins/skin_14.tscn", //14
        "res://player/skins/skin_15.tscn", //15
        "res://player/skins/skin_16.tscn", //16
        "res://player/skins/skin_17.tscn", //17
        "res://player/skins/skin_18.tscn", //18
        "res://player/skins/skin_19.tscn", //19
    };
    public bool[] skins_unlocked={
        true, //0
        false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
        false, //12
        false, //13
        false, //14
        false, //15
        false, //16
        false, //17
        false, //18
        false, //19
    };
    public bool[] skins_secret={
        false, //0
        false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
        false, //12
        false, //13
        false, //14
        false, //15
        false, //16
        false, //17
        false, //18
        false, //19
    };

    public int[] skins_rarity={ //0=common 1=rare 2=epic 3=legendary 4=divine 5=mythical
        0, //0
        0, //1
        0, //2
        3, //3
        1, //4
        1, //5
        2, //6
        2, //7
        3, //8
        2, //9
        1, //10
        0, //11
        1, //12
        2, //13
        1, //14
        3, //15
    };

    public string[] skins_recup={
        "none", //0
        "arcade", //1
        "arcade", //2
        "arcade", //3
        "arcade", //4
        "arcade", //5
        "arcade", //6
        "arcade", //7
        "pay", //8
        "pay", //9
        "arcade", //10
        "arcade", //11
        "arcade", //12
        "arcade", //13
        "arcade", //14
        "arcade", //15
        "arcade", //16
        "arcade", //17
        "arcade", //18
        "arcade", //19
    };

    // cost if recup="pay", level if recup="level", else 0
    public int[] skins_value={
        0, //0
        0, //1
        0, //2
        0, //3
        0, //4
        0, //5
        0, //6
        0, //7
        9999, //8
        1000, //9
        0, //10
        0, //11
        0, //12
        0, //13
        0, //14
        0, //15
        0, //16
        0, //17
        0, //18
        0, //19
    };

    //levels
    public string[] levels_names={
        "tuto-1", //0
        "tuto-2", //1
        "tuto-3", //2
        "tuto-4", //3
        "tuto-5", //4
        "Little platforms", //5
        "Another little platforms", //6
        "I don't want to fall !", //7
        "It is high !", //8
        "I need to jump !", //9
        "The dangerous road", //10
        "Thick lines", //11
    };

    public string[] levels_envs={
        "normal", //0
        "normal", //1
        "normal", //2
        "normal", //3
        "normal", //4
        "normal", //5
        "normal", //6
        "normal", //7
        "normal", //8
        "normal", //9
        "normal", //10
        "normal", //11
        "normal", //12
        "normal", //13
        "normal", //14
        "normal", //15
        "normal", //16
        "normal", //17
        "normal", //18
        "normal", //19
        "normal", //20
        "normal", //21
    };

    public int[] levels_nb_fixes={
        0, //0
        0, //1
        0, //2
        0, //3
        0, //4
        0, //5
        0, //6
        0, //7
        0, //8
        0, //9
        0, //10
        0, //11
        0, //12
    };

    public string[] levels_category={
        "tutos", //0
        "tutos", //1
        "tutos", //2
        "tutos", //3
        "tutos", //4
        "levels", //5
        "levels", //6
        "levels", //7
        "levels", //8
        "levels", //9
        "memory-error", //10
        "levels", //11
    };

    public string[] levels_path={
        "res://levels/aventures/tuto/tuto1.tscn", //0
        "res://levels/aventures/tuto/tuto2.tscn", //1
        "res://levels/aventures/tuto/tuto3.tscn", //2
        "res://levels/aventures/tuto/tuto4.tscn", //3
        "res://levels/aventures/tuto/tuto5.tscn", //4
        "res://levels/aventures/levels/level1.tscn", //5
        "res://levels/aventures/levels/level2.tscn", //6
        "res://levels/aventures/levels/level3.tscn", //7
        "res://levels/aventures/levels/level4.tscn", //8
        "res://levels/aventures/levels/level5.tscn", //9
        "res://levels/aventures/levels/level6.tscn", //10
        "res://levels/aventures/levels/level7.tscn", //11
    };
    public int[] levels_requirements={ //id of required level, -1 = not required
        -1, //0
        0, //1
        1, //2
        2, //3
        3, //4
        -1, //5
        5, //6
        6, //7
        7, //8
        8, //9
        -1, //10
        9, //11
    };
    public bool[] levels_finis={
        false, //0
        false, //1
        false, //2
        false, //3
        false, //4
        false, //5
        false, //6
        false, //7
        false, //8
        false, //9
        false, //10
        false, //11
        false, //12
        false, //13
        false, //14
        false, //15
        false, //16
        false, //17
        false, //18
        false, //19
        false, //20
        false, //21
        false, //22
        false, //23
        false, //24
        false, //25
        false, //26
        false, //27
        false, //28
        false, //29
        false, //30
        false, //31
        false, //32
        false, //33
        false, //34
    };
    public float[] levels_time={
        80, //0
        80, //1
        80, //2
        80, //3
        80, //4
        80, //5
        80, //6
        80, //7
        100, //8
        200, //9
        600, //10
        200, //11
        80, //12
        80, //13
    };
    public int[] levels_recomp_ncubes={
        10, //0
        20, //1
        30, //2
        40, //3
        50, //4
        50, //5
        50, //6
        50, //7
        50, //8
        50, //9
        50, //10
        50, //11
    };
    public int[,,] levels_recomp_skins={
        {}, //0
        {}, //1
        {}, //2
        {}, //3
        {}, //4
        {}, //5
        {}, //6
        {}, //7
        {}, //8
        {}, //9
        {}, //10
        {}, //11
    };
    //

    public string actu_cat="";
    public Player player;
    public level levele;
    public finNiv finnive;
    public Timer timer_message;
    public string controller_mode="keyboard";
    public LoadingScreen loadingScreen;
    public bool is_loading=false;
    [Signal]
    public delegate void playerDeath();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        //
        LoadGame();
        //
        timer_message=new Timer();
        timer_message.WaitTime=0.2F;
        AddChild(timer_message);
        timer_message.Connect("timeout", this, nameof(on_timer_message));
        timer_message.Start();
    }

    public string arcalcenv(){
        if(level<=3){ return "lava"; }
        else if(level<=8){ return "lava_rock"; }
        else if(level<=15){ return "rock_lava"; }
        else if(level<=20){ return "stone_dirt"; }
        else{ return "normal"; }
    }

    public void chargement_fini(){
        RemoveChild(loadingScreen);
        loadingScreen.QueueFree();
        loadingScreen=null;
        is_loading=false;
    }

    public void chargement(string path){
        if(!is_loading){
            is_loading=true;
            PackedScene packed = ResourceLoader.Load("res://menus/LoadingScreen.tscn") as PackedScene;
            loadingScreen = (LoadingScreen)packed.Instance();
            loadingScreen.Connect("chargement_fini", this, nameof(chargement_fini));
            AddChild(loadingScreen);
            loadingScreen.goto_scene(path);
        }
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
        //return true;
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
