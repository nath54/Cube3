using Godot;
using System;

public class Message : Panel
{
    public string titre = "title";
    public string message = "message";
    public string txt_bt = "ok";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Label ltitre = (Label)GetNode("Titre");
        Label lmessage = (Label)GetNode("Message");
        Button button = (Button)GetNode("Button");
        //
        ltitre.Text=titre;
        lmessage.Text=message;
        button.Text=txt_bt;
    }

    public void quite(){
        Node parent = GetParent();
        parent.RemoveChild(this);
    }

}
