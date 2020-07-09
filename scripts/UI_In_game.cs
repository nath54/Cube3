using Godot;
using System;

public class UI_In_game : Control
{
    private Label fps_counter;
    private Label aff_level;
    private Label debug;
    private ProgressBar time_left;
    private StyleBoxFlat pbcl;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        fps_counter = (Label)GetNode("fps_counter");
        aff_level = (Label)GetNode("aff_level");
        time_left = (ProgressBar)GetNode("time_left");
        debug = (Label)GetNode("debug");
        pbcl = (StyleBoxFlat)time_left.Get("custom_styles/fg");
    }

    public void changeDebugText(string text){
        debug.Text=text;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        fps_counter.Text="fps : "+Convert.ToString(Engine.GetFramesPerSecond());
    }

    public void changePercentBar(float percent){
        time_left.Value=percent;
        time_left.GetStylebox("Fg", "StyleBoxFlat");
        pbcl.BgColor = new Color(1-1*percent/100.0F,1*percent/100.0F,0);
    }

}
