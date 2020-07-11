
using Godot;
using System;

public class MenuSkins : Control
{
    public Global globale;
    public HBoxContainer container;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        //
        container = (HBoxContainer)GetNode("ScrollContainer/HBoxContainer");
        //
        createSkins();
    }

    public void createSkins(){
        for(int w=0; w<globale.max_skin; w++){
            Control skin_s=new Control();
            Godot.Label label = new Godot.Label();
            label.Text = "Skin n°"+w;
            skin_s.AddChild(label);
            ButtonSkinSelector bt = new ButtonSkinSelector();
            bt.id=w;
            if(globale.skin_id_equipe==w){
                bt.Text = "Selected";
                bt.Disabled=true;
            }
            else{
                bt.Text = "Select";
                bt.Disabled=false;
            }
            bt.Connect("clique", this, nameof(selectSkin));
            skin_s.AddChild(bt);
            container.AddChild(skin_s);
        }
    }

    public void selectSkin(int idskin){
        globale.skin_id_equipe=idskin;
        GetTree().ChangeScene("res://menus/MenuSkin.tscn");
    }

}
