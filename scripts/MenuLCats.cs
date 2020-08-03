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
        GD.Print("categories : ",sel_lvls.Count);
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
        element.Name="element-"+sel_levels[idl];
        //on crée le bouton pour acceder au level
        PackedScene packedScene = (PackedScene)ResourceLoader.Load("res://menus/buttons/Bt_Cube_Base.tscn");
        Bt_Cube button = (Bt_Cube)packedScene.Instance();
        button.texte=sel_levels[idl];
        button.id=idl;
        button.Connect("cliqued", this, nameof(on_level_pressed));
        element.AddChild(button);        
        //on le retourne
        return element;
    }

    public void createElements(){
        for(int idcat=0; idcat<sel_levels.Count; idcat++){
            Control element = create_Element(idcat);
            container.AddChild(element);
        }
        globale.affNode(container, 0);
    }

    public void on_hsb_changed(float value){
        container.RectPosition=new Vector2(value*size_element, 0);
    }

    public void on_level_pressed(int idl){
        globale.actu_cat=sel_levels[idl];
        GetTree().ChangeScene("res://menus/MenuLevels.tscn");
    }
    public void _on_Bt_back_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

}
