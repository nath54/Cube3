
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
        for(int w=0; w<=globale.max_skin; w++){
            Sprite skin_s = new Sprite();
            skin_s.Texture = ResourceLoader.Load("res://imgs/menu_skin_entity.png") as Texture;
            Godot.Label label = new Godot.Label();
            label.RectPosition=new Vector2(-18,-65);
            label.Text = "Skin n°"+w;
            skin_s.AddChild(label);
            ButtonSkinSelector bt = new ButtonSkinSelector();
            bt.id=w;
            if(globale.skin_id_equipe==w){
                bt.Text = "Selected";
                bt.Disabled=true;
                bt.RectPosition=new Vector2(-35,50);
            }
            else{
                bt.Text = "Select";
                bt.Disabled=false;
                bt.RectPosition=new Vector2(-28,50);
            }
            bt.Connect("clique", this, nameof(selectSkin));
            skin_s.AddChild(bt);
            skin_s.Scale=new Vector2(2,2);
            skin_s.Position=new Vector2(100+w*110*skin_s.Scale.x,200);
            container.AddChild(skin_s);
        }
    }

    public void selectSkin(int idskin){
        GD.Print("click "+idskin);
        globale.skin_id_equipe=idskin;
        GetTree().ChangeScene("res://menus/MenuSkin.tscn");
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
