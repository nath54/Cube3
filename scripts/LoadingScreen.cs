using Godot;
using System;

public class LoadingScreen : Control
{
    public AnimationPlayer animationPlayer;
    public ProgressBar progressBar;
    public float new_percente=0.0F;
    public int wait_frames;
    public float time_max = 100; //# msec
    public Godot.ResourceInteractiveLoader loader;
    public Label l_description;
    public Node current_scene;
    public Global globale;
    public bool is_charging=false;
    public string[] tips={
        "Building a cube world...",
        "Cubes modeling in progress ...",
        "If you like this game, do not hesitate to go to https://github.com/nath54/Cube3 and put a star to the project :)",
    };

    [Signal]
    public delegate void chargement_fini(LoadingScreen l);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        globale=(Global)GetNode("/root/Global");
        //
        SetProcess(false);
        Visible=false;
        //
    }
    
    public void _on_LoadingScreen_gui_input(InputEvent @event){
        GD.Print(@event);
        if(@event is InputEventScreenTouch){
            update_tips();
        }
    }

    public void update_tips(){
        Label l_description = (Label)GetNode("L_Description");
        Random rand = new Random();
        l_description.Text=tips[rand.Next(0,tips.Length)];
    }

    public void goto_scene(string path){  // Game requests to switch to this scene.
        //
        animationPlayer=(AnimationPlayer)GetNode("LoadScreenAnim");
        progressBar=(ProgressBar)GetNode("ProgressBar");
        //
        animationPlayer.Play("show");
        //
        Node root = GetTree().Root;
        //current_scene = root.GetChild(root.GetChildCount() -1);
        foreach(Node child in root.GetChildren()){
            if(!(child is Global) && !(child is GenerateArcade)){
                child.QueueFree();
            }
        }
        loader = ResourceLoader.LoadInteractive(path);
        is_charging=true;
        if(loader == null){ // Check for errors.
            //show_error();
            return;
            //SetProcess(true);
        }

        
        //current_scene.QueueFree(); // Get rid of the old scene.
                
        // Start your "loading..." animation.
        animationPlayer.Play("loading");
        //
        update_tips();
        //
        wait_frames = 1;
        //
        SetProcess(true);
    }    

    public override void _Process(float time){
        animationPlayer=(AnimationPlayer)GetNode("LoadScreenAnim");
        progressBar=(ProgressBar)GetNode("ProgressBar");
        //
        if(loader == null){
            //no need to process anymore
            SetProcess(false);
            return;
        }
        //Wait for frames to let the "loading" animation show up.
        if(wait_frames > 0){
            wait_frames -= 1;
            return;
        }
        var t = OS.GetTicksMsec();
        // Use "time_max" to control for how long we block this thread.
        while(OS.GetTicksMsec() < t + time_max){
            // Poll your loader.
            var err = loader.Poll();
            if(err.ToString() == "FileEof"){ // Finished loading.
                PackedScene resource = loader.GetResource() as PackedScene;
                loader.Dispose();
                loader = null;
                Visible=false;
                set_new_scene(resource);
                break;
            }
            else if(err.ToString() == "Ok"){
                update_progress();
            }
            else{ // Error during loading.
                //show_error();
                loader = null;
                break;
            }
        }            
    }
    
    public void update_progress(){
        animationPlayer=(AnimationPlayer)GetNode("LoadScreenAnim");
        progressBar=(ProgressBar)GetNode("ProgressBar");
        //
        var progress = (float)(loader.GetStage()) / loader.GetStageCount();
        // Update your progress bar?
        progressBar.Value=progress*100.0;
        // ...or update a progress animation?
        var length = animationPlayer.CurrentAnimationLength;
        // Call this on a paused animation. Use "true" as the second argument to
        // force the animation to update.
        animationPlayer.Seek(progress * length, true);
    }
    
    public void set_new_scene(PackedScene scene_resource){
        try{
            
            //GD.Print("loading scene resource : ",scene_resource);
            GetTree().ChangeSceneTo(scene_resource);
            //current_scene = scene_resource.Instance();
            //GetTree().Root.AddChild(current_scene);
            
            EmitSignal("chargement_fini");
        }
        catch{
            GD.Print("EEEEEEEEEEEEEEEERRRRRRRRRRRRRRRRROOOOOOOOOOOOOOOOOORRRRRRRRR");
        }
    }

}
