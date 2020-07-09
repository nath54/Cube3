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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
}
