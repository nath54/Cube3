using Godot;
using System;

public class Joystick_Button : TouchScreenButton
{
    public Vector2 radius = new Vector2(20,20);
    public float boundary = 100;
    public Int32 ongoing_drag = -1;

    public float return_accel = 20;
    public float threshold = 10;

    public override void _Process(float delta){
        if(ongoing_drag==-1){
            Vector2 pos_difference = (new Vector2(0,0)-radius*GlobalScale) - Position;
        }
    }

    public Vector2 get_button_pos(){
        return Position + radius * GlobalScale;
    }


    public override void _Input(InputEvent @event){
        if(@event is InputEventScreenDrag ie){
            float event_dist_from_center = (ie.Position - ((Sprite)GetParent()).GlobalPosition).Length();
            if(event_dist_from_center <= boundary * GlobalScale.x ||  ie.Index==ongoing_drag){
                GlobalPosition = ie.Position - radius * GlobalScale;
                if(get_button_pos().Length() > boundary){
                    Position = get_button_pos().Normalized()*boundary - radius;
                }
                GD.Print((ie.Index.GetType()));
                ongoing_drag=ie.Index;
            }
        }
        if(@event is InputEventScreenTouch iet && iet.IsPressed()){
            float event_dist_from_center = (iet.Position - ((Sprite)GetParent()).GlobalPosition).Length();
            if(event_dist_from_center <= boundary * GlobalScale.x ||  iet.Index==ongoing_drag){
                GlobalPosition = iet.Position - radius * GlobalScale;
                if(get_button_pos().Length() > boundary){
                    Position = get_button_pos().Normalized()*boundary - radius;
                }
                ongoing_drag=iet.Index;
            }
        }
        if(@event is InputEventScreenTouch iett && !iett.IsPressed() && iett.Index==ongoing_drag){
            ongoing_drag=-1;
        }
    }

    public Vector2 get_value(){
        if(get_button_pos().Length() > threshold){
            return get_button_pos().Normalized();
        }
        return new Vector2(0,0);
    }

		
}
