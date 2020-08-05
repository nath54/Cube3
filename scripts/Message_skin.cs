using Godot;
using System;

public class Message_skin : Panel
{
    public string titre = "You unlock this skin !";
    public int idskin=0;
    public string txt_bt = "ok";
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale=(Global)GetNode("root/Global");
        //
        Label ltitre = (Label)GetNode("Titre");
        Label lmessage = (Label)GetNode("Message");
        Button button = (Button)GetNode("Button");
        Sprite sprite = (Sprite)GetNode("Sprite");
        Sprite preview = (Sprite)GetNode("Preview");
        //
        if(idskin<globale.max_skin){
            ltitre.Text=titre;
            lmessage.Text=globale.skins_names[idskin];
            button.Text=txt_bt;
            sprite.Texture = ResourceLoader.Load("res://imgs/menu_skin_"+globale.rarities[globale.skins_rarity[idskin]]+".png") as Texture; 
            preview.Texture  = ResourceLoader.Load(globale.skins_preview[idskin]) as Texture; 
        }
        else{
            _on_Button_pressed();
        }

    }

    public void _on_Button_pressed(){
        Node parent = GetParent();
        parent.RemoveChild(this);
    }

}
