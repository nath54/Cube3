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

    //fonction pour débugger
    void affNode(Node node, Node parent,int iterations){
        string txt="  -";
        string tt="";
        for(int i=0; i<=iterations; i++){ txt="  "+txt; }
        if(node is Spatial sn){ tt=" ( x:"+sn.Translation.x+", y:"+sn.Translation.y+", z:"+sn.Translation.z+")"; }
        GD.Print(txt,node," : ", node.Name,tt);
        Godot.Collections.Array children = node.GetChildren();
        foreach(Node n in children){
            affNode(n,node,iterations+1);
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
        };
    }

    public void SaveGame(){
        var saveGame = new File();
        saveGame.Open("user://savegame.save", (Godot.File.ModeFlags)File.ModeFlags.Write);
        

        //on sauvegarde le global
        var nodeData=Save();
        saveGame.StoreLine(JSON.Print(nodeData));


        /*
        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes){
            // Check the node is an instanced scene so it can be instanced again during load.
            if (saveNode.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saveNode.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saveNode.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saveNode.Name));
                continue;
            }

            // Call the node's save function.
            var nodeData = saveNode.Call("Save");

            // Store the save dictionary as a new line in the save file.
            saveGame.StoreLine(JSON.Print(nodeData));
        }
        */

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
                skin_id_equipe=(int)nodeData["skin_id_equipe"];
                ms_cam=(int)nodeData["ms_cam"];
            }
        }

        /*
        while (saveGame.GetPosition() < save_game.GetLen())
        {
            // Get the saved dictionary from the next line in the save file
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
        
            // Firstly, we need to create the object and add it to the tree and set its position.
            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            var newObject = (Node)newObjectScene.Instance();
            GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
            newObject.Set("Position", new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

            // Now we set the remaining variables.
            foreach (KeyValuePair<object, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
                    continue;
                newObject.Set(key, entry.Value);
            }
        }
        */

        saveGame.Close();
    }

}
