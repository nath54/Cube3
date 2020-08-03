using System.Collections.Generic;
using System.Linq;
using Godot;
using System;

public class MenuLevels : Control
{
    public Global globale;
    public HScrollBar hsb;
    public Control container;
    public Label titre;
    public float container_size=0;
    public int[] sel_levels;
    public float size_element=500;
    public int nb_bt_placed=0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale = (Global)GetNode("/root/Global");
        //
        hsb=(HScrollBar)GetNode("HScrollBar");
        container=(Control)GetNode("Main_Container/Container");
        titre=(Label)GetNode("Titre");
        //
        List<int> sel_lvls = new List<int>();
        
        int idl=0;
        foreach(string lcat in globale.levels_category){
            if(lcat==globale.actu_cat){
                sel_lvls.Add(idl);
            }
            idl++;
        }
        sel_levels = sel_lvls.ToArray();
        GD.Print("levels : ",sel_levels);
        //
        container_size = 500*sel_levels.Length;
        hsb.MaxValue=sel_levels.Length;
        hsb.Value=container.RectPosition.x/size_element;
        hsb.Page=1;
        //
        createElements();
    }

    public Control create_Element(int idl){
        //on crée l'element 
        Control element=new Control();
        element.Name="element-"+globale.levels_names[idl];
        //on crée le bouton pour acceder au level
        if(globale.levels_finis[idl]){
            PackedScene packedScene = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Cube_Base.tscn");
            Bt_Cube button = (Bt_Cube)packedScene.Instance();
            button.texte=globale.levels_names[idl];
            button.id=idl;
            button.Connect("cliqued", this, nameof(on_level_pressed));
            element.AddChild(button);
        }
        else{
            if(globale.levels_requirements[idl]==-1 || globale.levels_finis[globale.levels_requirements[idl]]){
                PackedScene packedScene = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Cube_Finished.tscn");
                Bt_Cube button = (Bt_Cube)packedScene.Instance();
                button.texte=globale.levels_names[idl];
                button.id=idl;
                button.Connect("cliqued", this, nameof(on_level_pressed));
                element.AddChild(button);
            }
            else{
                PackedScene packedScene = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Cube_Locked.tscn");
                Bt_Cube button = (Bt_Cube)packedScene.Instance();
                button.texte=globale.levels_names[idl];
                button.id=idl;
                button.Connect("cliqued", this, nameof(on_level_pressed));
                element.AddChild(button);
            }
        }
        
        //on le retourne
        return element;
    }

    public void createElements(){
        foreach(int idlevel in sel_levels){
            Control element = create_Element(idlevel);
            element.RectPosition=new Vector2(100+nb_bt_placed*250,0);
            nb_bt_placed++;
            container.AddChild(element);
        }
    }

    public void on_hsb_changed(float value){
        container.RectPosition=new Vector2(value*size_element, 0);
    }

    public void on_level_pressed(int idl){
        int required=globale.levels_requirements[idl];
        if(required==-1 || globale.levels_finis[required]){
            globale.tipe="levels";
            globale.actu_id_niv=idl;
            GetTree().ChangeScene(globale.levels_path[idl]);
        }
        else{
            //TODO : un popup qui dit : "You must finish this level : "+globale.levels_name[required]+" before this level !"
        }
    }

    public void _on_Bt_back_pressed(){
        GetTree().ChangeScene("res://menus/MenuLCats.tscn");
    }

}
