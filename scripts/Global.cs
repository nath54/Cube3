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
    public int max_skin=3;

    [Signal]
    public delegate void playerDeath();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
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


}
