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
    public Button bt_keyboard;
    public Button bt_controller;
    public string mode = "keyboard";
    public string[] liste_strings={"move_forward","move_backward","move_left","move_right","jump","respawn","suicide"};
    public bool[] exclusive_keyboard={true,true,true,true,false,false,false};
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale = (Global)GetNode("/root/Global");
        //
        changinge = (Panel)GetNode("pressAKey");
        //
        bt_controller=(Button)GetNode("Control/Bt_manette");
        bt_keyboard=(Button)GetNode("Control/Bt_keyboard");
        //
        update_bt_mode();
        update();
    }

    public void update_bt_mode(){
        if(globale.controller_mode=="keyboard"){
            bt_keyboard.Disabled=true;
            bt_controller.Disabled=false;
            Label l = (Label)GetNode("pressAKey/Label");
            l.Text="please press a key...";
        }
        else{
            bt_keyboard.Disabled=false;
            bt_controller.Disabled=true;
            Label l = (Label)GetNode("pressAKey/Label");
            l.Text="please press a button or move a joystick...";
        }
        update();
    }

    public void update(){
        for(int w=0; w<liste_strings.Length; w++){
            if(exclusive_keyboard[w] && globale.controller_mode=="controller"){
                Label name_key= (Label)GetNode("Control/Control/HBoxContainer/VB_Keys/Key"+(w+1));
                Label name_action= (Label)GetNode("Control/Control/HBoxContainer/VB_Actions/Action"+(w+1));
                Control bt_change= (Control)GetNode("Control/Control/HBoxContainer/VB_btchanges/Bt"+(w+1));
                name_key.Visible=false;
                name_action.Visible=false;
                bt_change.Visible=false;
            }
            else{
                Label name_key= (Label)GetNode("Control/Control/HBoxContainer/VB_Keys/Key"+(w+1));
                Label name_action= (Label)GetNode("Control/Control/HBoxContainer/VB_Actions/Action"+(w+1));
                Control bt_change= (Control)GetNode("Control/Control/HBoxContainer/VB_btchanges/Bt"+(w+1));
                name_key.Visible=true;
                name_action.Visible=true;
                bt_change.Visible=true;

                Godot.Collections.Array es_pt = InputMap.GetActionList(liste_strings[w]);
                Godot.Collections.Array es= new Godot.Collections.Array();
                foreach(InputEvent e in es_pt){
                    if(globale.controller_mode=="keyboard"){
                        if(e is InputEventKey){
                            es.Add(e);
                        }
                    }
                    else if(globale.controller_mode=="controller"){
                        if(e is InputEventJoypadButton){
                            es.Add(e);
                        }
                    }
                }
                string txt="";
                int i=0;

                foreach(InputEvent e in es){
                    string txte=e.AsText();
                    if(e is InputEventJoypadButton iemb){
                        txte="Joy Button "+iemb.ButtonIndex;
                    }
                    
                    if(i<es.Count-1){
                        txt+=txte+" - ";
                    }
                    else{
                        txt+=txte;
                    }
                    if(globale.controller_mode=="controller"){
                        GD.Print(e,txte);
                    }
                    i++;
                }
                name_key.Text=txt;
            }
            
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
    public void on_bt_change_6(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=5;
    }
    public void on_bt_change_7(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=6;
    }
    public void on_bt_change_8(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=7;
    }
    public void on_bt_change_9(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=8;
    }
    public void on_bt_change_10(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=9;
    }
    public void on_bt_change_11(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=10;
    }

    public void on_bt_keyboard(){
        globale.controller_mode="keyboard";
        update_bt_mode();
    }

    public void on_bt_controller(){
        globale.controller_mode="controller";
        update_bt_mode();
    }

    public override void _Input(InputEvent @event){
        if(changing){
            if(globale.controller_mode=="keyboard"){
                if(@event is InputEventKey){                
                    Godot.Collections.Array old_button= InputMap.GetActionList(liste_strings[input_changing]);
                    foreach(InputEvent e in old_button){
                        if(e is InputEventKey){
                            InputMap.ActionEraseEvent(liste_strings[input_changing],e);
                        }  
                    }
                    InputMap.ActionAddEvent(liste_strings[input_changing], @event);
                    changing=false;
                    changinge.Visible=changing;
                    update();
                }
            }
            else if(globale.controller_mode=="controller"){
                if(@event is InputEventJoypadButton ){                
                    Godot.Collections.Array old_button= InputMap.GetActionList(liste_strings[input_changing]);
                    foreach(InputEvent e in old_button){
                        if(@event is InputEventJoypadButton){
                            InputMap.ActionEraseEvent(liste_strings[input_changing],e);
                        }  
                    }
                    InputMap.ActionAddEvent(liste_strings[input_changing], @event);
                    changing=false;
                    changinge.Visible=changing;
                    update();
                }
            }
        }
    }

    public void _on_VScrollBar_value_changed(float value){
        HBoxContainer container = (HBoxContainer)GetNode("Control/Control/HBoxContainer");
        container.RectPosition=new Vector2(0,-value*50);
    }

    public void _on_Bt_back_pressed(){
        GetTree().ChangeScene("res://menus/Settings_game.tscn");
    }

}

