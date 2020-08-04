using System.Net.Mime;
using System.Collections.Generic;
using Godot;
using System;

public class Settings_Controls : Control
{
    public Label name_key_1;
    public Label name_key_2;
    public Label name_key_3;
    public Label name_key_4;
    public Label name_key_5;
    public bool changing=false;
    public int input_changing=-1;
    public Panel changinge;
    public string[] liste_strings={"move_forward","move_backward","move_left","move_right","jump"};

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        name_key_1 = (Label)GetNode("Control/HBoxContainer/VB_Keys/Key1");
        name_key_2 = (Label)GetNode("Control/HBoxContainer/VB_Keys/Key2");
        name_key_3 = (Label)GetNode("Control/HBoxContainer/VB_Keys/Key3");
        name_key_4 = (Label)GetNode("Control/HBoxContainer/VB_Keys/Key4");
        name_key_5 = (Label)GetNode("Control/HBoxContainer/VB_Keys/Key5");
        //
        changinge = (Panel)GetNode("pressAKey");
        //
        update();
    }

    public void update(){
        Label[] liste_labels={name_key_1,name_key_2,name_key_3,name_key_4,name_key_5};
        for(int w=0; w<liste_labels.Length; w++){
            Godot.Collections.Array es_pt = InputMap.GetActionList(liste_strings[w]);
            Godot.Collections.Array es= new Godot.Collections.Array();
            foreach(InputEvent e in es_pt){
                if(e is InputEventKey){
                    es.Add(e);
                }
            }
            string txt="";
            int i=0;

            foreach(InputEvent e in es){
                if(i<es.Count-1){
                    txt+=e.AsText()+" - ";
                }
                else{
                    txt+=e.AsText();
                }                
                i++;
            }
            liste_labels[w].Text=txt;
        }
    }

    public void on_bt_change_1(){
        changing=true;
        changinge.Visible=changing;
        input_changing=0;
    }
    public void on_bt_change_2(){
        changing=true;
        changinge.Visible=changing;
        input_changing=1;
    }
    public void on_bt_change_3(){
        changing=true;
        changinge.Visible=changing;
        input_changing=2;        
    }
    public void on_bt_change_4(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=3;
    }
    public void on_bt_change_5(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=4;
    }

    public void changing_key(){
        
    }

    public override void _Input(InputEvent @event){
        if(changing){
            if(@event is InputEventKey){                
                Godot.Collections.Array old_button= InputMap.GetActionList(liste_strings[input_changing]);
                foreach(InputEvent e in old_button){
                    InputMap.ActionEraseEvent(liste_strings[input_changing],e);
                }
                InputMap.ActionAddEvent(liste_strings[input_changing], @event);
                changing=false;
                changinge.Visible=changing;
                update();
            }
        }
    }

    public void _on_Bt_back_pressed(){
        GetTree().ChangeScene("res://menus/Settings_game.tscn");
    }

}

