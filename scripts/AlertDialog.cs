using Godot;
using System;

public class AlertDialog : Control
{
    public PopupDialog popup;
    public Label texte;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        popup=(PopupDialog)GetNode("PopupDialog");
        popup.Popup_();
        texte=(Label)GetNode("PopupDialog/Label");
    }

    public void setText(string text){
        texte.Text=text;
    }

    public void _on_Button_pressed(){
        popup.Hide();
        //GetTree().RemoveChild(this);
    }


}
