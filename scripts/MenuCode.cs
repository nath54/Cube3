using Godot;
using System;

public class MenuCode : Control
{
    public TextEdit textEdit;
    public Label txt_cool;
    public Panel popup;
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        textEdit = (TextEdit)GetNode("TextEdit");
        popup = (Panel)GetNode("popup");
        txt_cool = (Label)GetNode("popup/txt_cool");
        globale = (Global)GetNode("/root/Global");
    }

    public void popupe(String txt){
        popup.Visible=true;
        txt_cool.Text=txt;
    }

    public void _on_Bt_ok_pressed(){
        String code = textEdit.Text;
        if(code=="cubloween"){
            popupe("Cool ! Vous avez débloqué le skin pumpkin !");
            globale.skins_unlocked[15]=true;
            globale.SaveGame();
        }
    }

    public void _on_Bt_menu_pressed(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void _on_Bt_cool_pressed(){
        popup.Visible=false;
    }

}
