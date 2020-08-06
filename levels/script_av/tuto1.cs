using System.Linq;
using Godot;
using System;

public class tuto1 : Spatial
{
    public Label label;
    public Global globale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale =(Global)GetNode("/root/Global");
        //
        if(globale.is_mobile()){
            label=(Label)GetNode("Label");
            label.Text=@"You can move with the joystick.
            You must go in the portal to finish the level";        
        }
        else{
            string txt_inps="";
            string[] aes={"move_forward","move_backward","move_left","move_right"};
            int ri=0;   
            foreach(string ae in aes){
                Godot.Collections.Array iept = InputMap.GetActionList(ae);
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
                if(ri<aes.Length-1){
                    txt_inps+=", ";
                }
                ri++;
            }
            label=(Label)GetNode("Label");
            label.Text=@"You can move with "+txt_inps+@".
You must go in the portal to finish the level";
        }

    }
        
}
