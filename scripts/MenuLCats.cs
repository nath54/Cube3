using System.Collections.Generic;
using Godot;
using System;

public class MenuLCats : Control
{
    public Global globale;
    public HScrollBar hsb;
    public Control container;
    public Label titre;
    public float container_size=0;
    public List<string> sel_levels;
    public float size_element=500;

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
        List<string> sel_lvls = new List<string>();
        
        foreach(string lcat in globale.levels_category){
            if( !(sel_lvls.Contains(lcat)) ){
                sel_lvls.Add(lcat);
            }
        }
        sel_levels = sel_lvls;
        GD.Print("categories : ",sel_lvls);
        //

        container_size = 500*sel_levels.Count;
        hsb.MaxValue=sel_levels.Count;
        hsb.Value=container.RectPosition.x/size_element;
        hsb.Page=1;
        //
        createElements();
    }

    public Control create_Element(int idl){
        //on crée l'element 
        Control element=new Control();
        //on crée le bouton pour acceder au level
        IdButton button = new IdButton();
        button.Text=globale.levels_names[idl];
        button.id=idl;
        button.Connect("cliqued", this, nameof(on_level_pressed));
        //on le retourne
        return element;
    }

    public void createElements(){
        for(int idcat=0; idcat<sel_levels.Count; idcat++){
            Control element = create_Element(idcat);
            container.AddChild(element);
        }
    }

    public void on_hsb_changed(float value){
        container.RectPosition=new Vector2(value*size_element, 0);
    }

    public void on_level_pressed(int idl){
        int required=globale.levels_requirements[idl];
        if(required==-1 || globale.levels_finis[required]){
            GetTree().ChangeScene(globale.levels_path[idl]);
        }
        else{
            //TODO : un popup qui dit : "You must finish this level : "+globale.levels_name[required]+" before this level !"
        }
    }
    public void _on_Bt_back_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
