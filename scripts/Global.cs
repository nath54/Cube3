using Godot;
using System;

public class Global : Node
{
    
    public int level=1;
    public int mtx = 32;
    public int mtz = 32;
    public int mty = 32;
    public int nbchem=32;

    public float timemax=180;
    public int nb_plats=20;
    public string tipe="platforms";
    public float player_taille=1;
    public Vector3 grid_scale = new Vector3(1,1,1);
    public float grid_cell_scale = 1;
    public int skin_id_equipe=0;
    public int max_skin=8;
    public int ms_cam=0;
    public int highscore_plats=1;
    public bool aff_fps=true;
    public bool sao=false;
    public int sao_quality=0;
    public int actu_id_niv;

    // skins

    public string[] skins_names={
        "base",
        "smile",
        "smileye",
        "ssj",
        "carreaux",
        "furnace",
        "halo",
        "transblue"
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
        "arcade",
    };

    //levels

    public string[] levels_names={
        "tuto1"
    };
    public string[] levels_category={
        "tutos"
    };
    public string[] levels_path={
        "res://levels/aventures/tuto/tuto1.tscn",
    };
    public int[] levels_requirements={ //id of required level, -1 = not required
        -1,
    };
    public bool[] levels_finis={
        false,
    };

    //

    public string actu_cat="";

    public Player player;
    public level levele;
    public finNiv finnive;

    [Signal]
    public delegate void playerDeath();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        LoadGame();
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
            { "highscore_plats", ""+highscore_plats },
            { "aff_fps", aff_fps},
            { "sao", sao},
            { "sao_quality", sao_quality},

        };
    }

    public void SaveGame(){
        var saveGame = new File();
        saveGame.Open("user://savegame.save", (Godot.File.ModeFlags)File.ModeFlags.Write);
        

        //on sauvegarde le global
        var nodeData=Save();
        saveGame.StoreLine(JSON.Print(nodeData));
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
                if( nodeData.Keys.Contains("highscore_plats")){
                    highscore_plats=Convert.ToInt32(nodeData["highscore_plats"]);
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
            }
        }
        saveGame.Close();
    }

}
