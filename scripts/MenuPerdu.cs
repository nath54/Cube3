using System.Collections.Generic;
using System.Linq;
using Godot;
using System;

public class MenuPerdu : Control
{
    public Global globale;
    public Control skins_unlock;
    public Sprite skin_preview;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        if(!globale.is_mobile()){ Input.SetMouseMode(Input.MouseMode.Visible); }
        //
        skins_unlock=(Control)GetNode("Unlock_Skins");
        skin_preview=(Sprite)GetNode("Unlock_Skins/Preview");
        //
        string[] wtps={"maze","platforms"};
        if(wtps.Contains(globale.tipe)){
            unlock_skins();
        }
        //
        int recomp=0;
        if(wtps.Contains(globale.tipe)){
            Random rand = new Random();
            recomp=rand.Next(globale.level-20,globale.level+20);
            if(globale.difficulty==0){
                recomp=Convert.ToInt32(recomp/1.5);
            }
            else if(globale.difficulty==2){
                recomp=Convert.ToInt32(recomp*1.5);
            }
            else if(globale.difficulty==3){
                recomp=Convert.ToInt32(recomp*3);
            }
            if(recomp<1){ recomp=1; }
        }
        Label l_recomp = (Label)GetNode("L_recomp");
        l_recomp.Text="You earn : "+recomp;
        globale.ncubes+=recomp;
        globale.SaveGame();
    }

    public void unlock_skins(){
        
        int lvl = globale.level;
        int rar=0;
        if(lvl>=10){ rar=1; }
        if(lvl>=25){ rar=2; }
        if(lvl>=50){ rar=3; }
        if(lvl>=80){ rar=4; }
        if(lvl>=100){ rar=5; }
        //
        int idskin=-1;
        //
        while(idskin==-1 && rar>=0){
            List<int> skins_dispo=new List<int>();
            for(int ids=0; ids<globale.skins_names.Length; ids++){
                if(globale.skins_rarity[ids]==rar && !globale.skins_unlocked[ids] && globale.skins_recup[ids]=="arcade"){
                    skins_dispo.Add(ids);
                }
            }
            if(skins_dispo.Count>0){
                Random random=new Random();
                int i=random.Next(0, skins_dispo.Count);
                idskin=skins_dispo[i];
            }
            else{
                rar--;
            }
        }
        //
        if(idskin!=-1){
            skins_unlock.Visible=true;
            skin_preview.Texture=ResourceLoader.Load(globale.skins_preview[idskin]) as Texture;
            globale.skins_unlocked[idskin]=true;
        }
    }

    public void _on_Bt_Menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void _on_Bt_Quit_pressed(){
        GetTree().Quit();
    }

}
