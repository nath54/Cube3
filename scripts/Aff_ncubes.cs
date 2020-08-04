using Godot;
using System;

public class Aff_ncubes : Control
{
    public Global globale;
    public Label label;
    public Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale = (Global)GetNode("/root/Global");
        label = (Label)GetNode("Label");
        label.Text = ""+globale.ncubes;
        //
        timer = (Timer)GetNode("Timer");
        timer.Start();
    }
    public void _on_Timer_timeout(){
        label.Text = ""+globale.ncubes;
        timer.Start();
    }
}
