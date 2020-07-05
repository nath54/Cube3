using Godot;
using System;

public class SwipeDetector : Node
{
    
    private Timer timer;
    private Vector2 swipe_start_position;

    public float MAX_DIAGONAL_SLOPE = 1.3F;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = (Timer)GetNode("Timer");
    }

    public override void _Input(InputEvent @event){
        if(@event is InputEventScreenTouch){
            InputEventScreenTouch e = (InputEventScreenTouch)@event;
            if(e.IsPressed()){
                _start_detection(e.Position);
            }
            else if(!timer.IsStopped()){
                _end_detection(e.Position);
            }
        }
    }

    public void _start_detection(Vector2 pos){
        swipe_start_position=pos;
        timer.Start();
    }

    public void _end_detection(Vector2 pos){
        timer.Stop();
        Vector2 direction = (pos - swipe_start_position).Normalized();
        if(Math.Abs(direction.x) + Math.Abs(direction.y) > MAX_DIAGONAL_SLOPE ){
            return ;
        }
        if(Math.Abs(direction.x) > Math.Abs(direction.y)){
            Vector2 par;
            par.x=-Math.Sign(direction.x);
            par.y=0.0F;
            EmitSignal("swiped", par);
        }
        else{
            Vector2 par;
            par.x=-Math.Sign(direction.x);
            par.y=0.0F;
            EmitSignal("swiped", par);
        }


    }

    public void _onTimerTimeout(){
        EmitSignal("swipe_canceled", swipe_start_position);
    }

}
