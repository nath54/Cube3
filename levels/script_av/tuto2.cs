using Godot;
using System;

public class tuto2 : Spatial
{
    public Label label;
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        //
        globale = (Global)GetNode("/root/Global");
        //
        if(globale.is_mobile()){

            label=(Label)GetNode("Label");
            label.Text="You can jump with the jump button.";
        }
        else{
            string txt_inps="";
            Godot.Collections.Array iept = InputMap.GetActionList("jump");
            Godot.Collections.Array ie=new Godot.Collections.Array();
            foreach(InputEvent inpe in iept){
                if(inpe is InputEventKey){
                    ie.Add(inpe);
                }
            }
            for(int i=0; i<ie.Count; i++){
                txt_inps+=((InputEvent)ie[i]).AsText();
                if(i<ie.Count-1){
                    txt_inps+=" - ";
                }
            }
            label=(Label)GetNode("Label");
            label.Text="You can jump with "+txt_inps+"";
        }
        
    }
}
