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
    }

    public void update_skins(){
        for(int w=0; w<3; w++){
            int ids=w+globale.ms_cam;
            string idd=""+(ids+1);
            while(idd.Length<2){
                idd="0"+idd;
            }
            Sprite spritee = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Sprite");
            spritee.Texture=ResourceLoader.Load("res://imgs/menu_skin_"+globale.rarities[globale.skins_rarity[ids]]+".png") as Texture;
            Control controle = (Control)GetNode("Container/Skin_0"+(w+1));
            Bt_Rect boutton = (Bt_Rect)GetNode("Container/Skin_0"+(w+1)+"/Button");
            if(globale.skins_unlocked[ids]){
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");                
                name.Text=globale.skins_names[ids];                
                preview.Texture=ResourceLoader.Load(globale.skins_preview[ids]) as Texture;
                if(ids==globale.skin_id_equipe){
                    PackedScene pack = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Rect_Selected.tscn");
                    Bt_Rect bouton = (Bt_Rect)pack.Instance();
                    bouton.Name="Button";
                    bouton.texte="select";
                    bouton.id=ids;
                    bouton.Connect("clique", this, nameof(selectSkin));
                    controle.AddChild(bouton);
                    bouton.RectPosition= new Vector2(-40, 49);
                }
                else{
                    PackedScene pack = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Rect_Base.tscn");
                    Bt_Rect bouton = (Bt_Rect)pack.Instance();
                    bouton.Name="Button";
                    bouton.texte="select";
                    bouton.id=ids;
                    bouton.Connect("clique", this, nameof(selectSkin));
                    controle.AddChild(bouton);
                    bouton.RectPosition= new Vector2(-40, 49);
                }
            }
            else if(globale.skins_recup[ids]=="pay"){
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                preview.Texture=ResourceLoader.Load(globale.skins_preview[ids]) as Texture;
                name.Text=globale.skins_names[ids];
                PackedScene pack = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Rect_Pay.tscn");
                Bt_Rect bouton = (Bt_Rect)pack.Instance();
                bouton.Name="Button";
                bouton.texte=""+globale.skins_value[ids];
                bouton.id=ids;
                bouton.Connect("clique", this, nameof(selectSkin));
                controle.AddChild(bouton);
                bouton.RectPosition= new Vector2(-40, 49);
            }
            else if(!globale.skins_secret[ids]){
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                preview.Texture=ResourceLoader.Load(globale.skins_preview[ids]) as Texture;
                name.Text=globale.skins_names[ids];
                PackedScene pack = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Rect_Disabled.tscn");
                Bt_Rect bouton = (Bt_Rect)pack.Instance();
                bouton.Name="Button";
                bouton.texte="locked";
                bouton.id=ids;
                bouton.Connect("clique", this, nameof(selectSkin));
                controle.AddChild(bouton);
                bouton.RectPosition= new Vector2(-40, 49);
            }
            else{
                Label name = (Label)GetNode("Container/Skin_0"+(w+1)+"/Name");
                Sprite preview = (Sprite)GetNode("Container/Skin_0"+(w+1)+"/Preview");
                name.Text="secret";
                preview.Texture=ResourceLoader.Load("res://img_skins/secret.png") as Texture;
                PackedScene pack = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Rect_Disabled.tscn");
                Bt_Rect bouton = (Bt_Rect)pack.Instance();
                bouton.Name="Button";
                bouton.texte="locked";
                bouton.id=ids;
                bouton.Connect("clique", this, nameof(selectSkin));
                controle.AddChild(bouton);
                bouton.RectPosition= new Vector2(-40, 49);
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
        if(globale.skins_unlocked[idskin]){
            globale.skin_id_equipe=idskin;
            globale.SaveGame();
            GetTree().ChangeScene("res://menus/MenuSkins.tscn");
        }
        else if(globale.skins_recup[idskin]=="pay"){
            if(globale.ncubes>=globale.skins_value[idskin]){
                globale.ncubes-=globale.skins_value[idskin];
                globale.skins_unlocked[idskin]=true;
            }
            else{
                Message mes = new Message();
                mes.titre="Warning !";
                mes.message="You don't have enuch money to buy this skin !";
                AddChild(mes);
            }
        }
        else{
            //TODO : afficher un message qui dit comment le débloquer
            Message mes = new Message();
            mes.titre="Skin Locked !";
            mes.message="";
            if(globale.skins_recup[idskin]=="level_finish"){
                mes.message="You need finish the level "+globale.levels_names[globale.skins_value[idskin]]+" of "+globale.levels_category[globale.skins_value[idskin]]+" to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="level_finf"){
                mes.message="You need to find this  skin in the level "+globale.levels_names[globale.skins_value[idskin]]+" of "+globale.levels_category[globale.skins_value[idskin]]+" to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="arcade"){
                string[] lvl_rar={"1","10","25","50","80","100","1000"};
                mes.message="You need to raich the level "+lvl_rar[globale.skins_rarity[idskin]]+" to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="arcade-easy"){
                string[] lvl_rar={"1","10","25","50","80","100","1000"};
                mes.message="You need to raich the level "+lvl_rar[globale.skins_rarity[idskin]]+" in easy mode to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="arcade-normal"){
                string[] lvl_rar={"1","10","25","50","80","100","1000"};
                mes.message="You need to raich the level "+lvl_rar[globale.skins_rarity[idskin]]+" in normal mode to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="arcade-hard"){
                string[] lvl_rar={"1","10","25","50","80","100","1000"};
                mes.message="You need to raich the level "+lvl_rar[globale.skins_rarity[idskin]]+" in hard mode to unlock this skin.";
            }
            else if(globale.skins_recup[idskin]=="arcade-hell"){
                string[] lvl_rar={"1","10","25","50","80","100","1000"};
                mes.message="You need to raich the level "+lvl_rar[globale.skins_rarity[idskin]]+" in hell mode to unlock this skin.";
            }
            AddChild(mes);
        }
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
