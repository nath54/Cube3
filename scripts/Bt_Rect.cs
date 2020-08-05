using Godot;
using System;

public class Bt_Rect : TextureButton
{
    public int id;
    public string texte;
    public Color cl = new Color(1,1,1,1);

    [Signal]
    public delegate void clique(int id);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Label text = (Label)GetNode("Label");
        text.Text=texte;
        text.AddColorOverride("font_color",cl);
        Connect("pressed", this, nameof(cliquer));
    }

    public void cliquer(){
        EmitSignal("clique", id);
    }
 
}
