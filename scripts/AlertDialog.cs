using System.Linq.Expressions;
using System.Reflection.Emit;
using Godot;
using System;

public class AlertDialog : Control
{
    public PopupDialog popup;
    public Label texte;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        text=(Label)GetNode("PopupDialog/Label");
        popup=(PopupDialog)GetNode("PopupDialog");
        popup.Popup_();
    }

    public void setText(string text){
        texte.Text=text;
    }

    public void on_button_quit_press(){
        popup.Hide();
        GetTree().RemoveChild(this);
    }
}
