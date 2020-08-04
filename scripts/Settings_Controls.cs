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

    }

    public void update(){
    }

    public void on_bt_change_1(){
        changing=true;
        changinge.Visible=changing;
        input_changing=1;
    }
    public void on_bt_change_2(){
        changing=true;
        changinge.Visible=changing;
        input_changing=2;
    }
    public void on_bt_change_3(){
        changing=true;
        changinge.Visible=changing;
        input_changing=3;        
    }
    public void on_bt_change_4(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=4;
    }
    public void on_bt_change_5(){        
        changing=true;
        changinge.Visible=changing;
        input_changing=5;
    }

    public void changing_key(){
        
    }

    public override void _Process(float delta){
        if(changing){
            if(true){
                changing=false;
                changinge.Visible=changing;
                update();
            }
        }
    }

}

