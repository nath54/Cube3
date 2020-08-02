using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

using Godot;
using System;

public class MenuSkins : Control
{
    public Global globale;
    public HScrollBar scrollBar;
    
    public string[] skin_names = {"base","smile","smileye","ssj","carreaux","furnace","halo","transp_blue"};

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        //
        scrollBar = (HScrollBar)GetNode("Container/HScrollBar");
        scrollBar.MaxValue=globale.max_skin;
        scrollBar.Value=globale.ms_cam;
        //
        update_skins();
        //
        for(int w=1; w<=3; w++){
            ButtonSkinSelector bouton = (ButtonSkinSelector)GetNode("Container/Skin_0"+w+"/Button");
            bouton.Connect("clique", this, nameof(selectSkin));
        }

        
    }

    public void update_skins(){
        for(int w=0; w<3; w++){
            int ids=w+globale.ms_cam;
            string idd=""+(ids+1);
            while(idd.Length<2){
                idd="0"+idd;
            }
            ButtonSkinSelector bouton = (ButtonSkinSelector)GetNode("Container/Skin_0"+(w+1)+"/Button");
            bouton.id=ids;
            if(globale.skins_unlocked[ids]){
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                
                name.Text=globale.skins_names[ids];
                
                preview.Texture=ResourceLoader.Load(globale.skins_preview[ids]) as Texture;
                if(ids==globale.skin_id_equipe){
                    bouton.Disabled=true;
                    bouton.Text="select";
                }
                else{
                    bouton.Disabled=false;
                    bouton.Text="select";
                }
            }
            else if(!globale.skins_secret[ids]){
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                preview.Texture=ResourceLoader.Load(globale.skins_preview[ids]) as Texture;
                name.Text=globale.skins_names[ids];
                bouton.Text="locked";
                bouton.Disabled=true;
            }
            else{
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                name.Text="secret";
                preview.Texture=ResourceLoader.Load("res://img_skins/secret.png") as Texture;
                bouton.Text="locked";
                bouton.Disabled=true;
            }
            
            
        }
    }

    public void _on_TBT_skin_next_pressed(){
        if(globale.ms_cam<globale.max_skin-3){
            globale.ms_cam+=1;
            scrollBar.Value=globale.ms_cam;
            globale.SaveGame();
            update_skins();
        }
    }
    public void _on_TBT_skin_previous_pressed(){
        if(globale.ms_cam>0){
            globale.ms_cam-=1;
            scrollBar.Value=globale.ms_cam;
            globale.SaveGame();
            update_skins();
        }
    }

    public void _on_HScrollBar_value_changed(float value){
        globale.ms_cam=Convert.ToInt32(value);
        globale.SaveGame();
        update_skins();
    }

    public void selectSkin(int idskin){
        //GD.Print("click "+idskin);
        globale.skin_id_equipe=idskin;
        globale.SaveGame();
        GetTree().ChangeScene("res://menus/MenuSkins.tscn");
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
