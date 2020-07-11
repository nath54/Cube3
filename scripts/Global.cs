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
    public int skin_id_equipe=3;
    public int max_skin=3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
}
